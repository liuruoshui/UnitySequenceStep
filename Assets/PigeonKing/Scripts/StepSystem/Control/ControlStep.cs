using System.Collections.Generic;
using UnityEngine;

namespace PigeonKingGames.Steps
{

    // controld step has children steps, can start children step
    public abstract class ControlStep : StepBase
    {

        public List<StepModifier> childrenModifiers = new List<StepModifier>();

        /// <summary>
        /// set in inspector, if check childStartWithNewObject, will use this list to generate children steps
        /// </summary>
        public List<StepBase> setChildrenSteps = new List<StepBase>();

        /// <summary>
        /// real running steps
        /// </summary>
        protected List<StepBase> childrenSteps = new List<StepBase>();

        /// <summary>
        /// current running steps
        /// </summary>
        public List<StepBase> currentSteps = new List<StepBase>();

        [Tooltip("auto destroy all set sequence steps when destroy this")]
        // auto destroy all setSequenceSteps
        public bool DestroyWhenDestroy = false;

        /// <summary>
        /// if true, will initialize actual sequence steps instead of source steps, for same step used multi times, don't change it when running, may cause bug
        /// </summary>
        [Tooltip("if true, will initialize actual sequence steps instead of source steps, for same step used multi times, don't change it when running, may cause bug")]
        public bool childStartWithNewObject = true;

        /// <summary>
        /// override BeforeInit, handle children steps instantiate or not
        /// </summary>
        /// <param name="parentStep"></param>
        /// <param name="lastStep"></param>
        protected override bool BeforeInit()
        {
            if (setChildrenSteps.Count == 0)
            {
                return false;
            }
            // deactive all setSequenceSteps
            foreach (var step in setChildrenSteps)
            {
                step.gameObject.SetActive(false);
            }

            if (childStartWithNewObject)
            {
                childrenSteps = new List<StepBase>();
                foreach (var step in setChildrenSteps)
                {
                    var newStep = Instantiate(step);
                    newStep.transform.SetParent(transform);
                    childrenSteps.Add(newStep);
                    // deactive all sequenceSteps
                    newStep.gameObject.SetActive(false);
                }
            }
            else
            {
                childrenSteps = setChildrenSteps;
            }
            return true;
        }

        public virtual void AddChildrenModifier(StepModifier modifier)
        {
            childrenModifiers.Add(modifier);
        }

        public virtual void removeChildrenModifier(int index)
        {
            if (index >= 0 && index < childrenModifiers.Count)
            {
                childrenModifiers.RemoveAt(index);
            }
        }

        public void AddChildrenStep(StepBase step)
        {
            childrenSteps.Add(step);
        }

        public virtual void AddChildrenStep(StepBase step, int index)
        {
            StepBase newStep;
            if (childStartWithNewObject)
            {
                setChildrenSteps.Add(step);
                step.gameObject.SetActive(false);
                newStep = Instantiate(step);
                newStep.gameObject.SetActive(false);
                newStep.transform.SetParent(transform);
            }
            else
            {
                newStep = step;
            }

            if (index < 0)
            {
                childrenSteps.Add(newStep);
            }
            else
            {
                childrenSteps.Insert(index, newStep);
            }
        }



        /// <summary>
        /// default before start child step, clear currentSteps,if need some other logic, override this
        /// </summary>
        /// <param name="index"></param>
        /// <param name="finishedChild"></param>
        protected virtual void beforeStartChildStep(int index, StepBase finishedChild)
        {
            currentSteps.Clear();
        }

        protected virtual void endStartChildStep(int index, StepBase finishedChild)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="finishedChild"></param>
        public virtual void StartChildStep(int index, StepBase finishedChild)
        {
            beforeStartChildStep(index, finishedChild);
            currentSteps.Add(childrenSteps[index]);
            childrenSteps[index].gameObject.SetActive(true);
            childrenSteps[index].AddFinishListner(ChildrenFinished);
            childrenSteps[index].Init();
            foreach (var stepmodifier in childrenModifiers)
            {
                stepmodifier?.ModifyStep(childrenSteps[index]);
            }
            childrenSteps[index].StartStep();
            finishedChild?.gameObject.SetActive(false);
        }

        /// <summary>
        /// control step logic when children step finished
        /// </summary>
        /// <param name="finishedChild"></param>
        public abstract void ChildrenFinished(StepBase finishedChild);

        protected override bool BeforeEnd()
        {
            foreach (var step in childrenSteps)
            {
                if (childStartWithNewObject)
                {
                    ClearOneStep(step);
                }
                else
                {
                    TerminateOneStep(step);
                }
            }
            return base.BeforeEnd();
        }
        /// <summary>
        /// remove all listeners
        /// </summary>
        private void OnDestroy()
        {
            foreach (var step in childrenSteps)
            {
                ClearOneStep(step);
            }
            foreach (var step in setChildrenSteps)
            {
                if (DestroyWhenDestroy)
                {
                    ClearOneStep(step);
                }
                else
                {
                    TerminateOneStep(step);
                }
            }
        }

        protected override bool BeforeStart()
        {
            if (childrenSteps.Count > 0)
            {
                return true;
            }
            else
            {
                EndStep();
                return false;
            }
        }


        protected override void AfterStart()
        {
            LaunchChildrenStart();
        }
        /// <summary>
        /// let children steps start
        /// </summary>
        protected abstract void LaunchChildrenStart();
    }
}