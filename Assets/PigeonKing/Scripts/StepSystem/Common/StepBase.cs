using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps
{
    public enum StepState
    {
        NotStarted,
        Running,
        Finished
    }

    // Base Step Class，Have a parentStep, and a OnStepFinished Event, will Invoke when Step Finished
    public class StepBase : MonoBehaviour
    {
        #region static method
        public static void ClearOneStep(StepBase step)
        {
            if (step != null)
            {
                step.OnStepFinished?.RemoveAllListeners();
                step.EndStep();
                step.gameObject.SetActive(false);
                Destroy(step.gameObject);
            }
        }

        public static void TerminateOneStep(StepBase step)
        {
            if (step != null)
            {
                step.OnStepFinished?.RemoveAllListeners();
                step.EndStep();
                step.gameObject.SetActive(false);
            }
        }

        #endregion static method

        [Tooltip("destroy self when step end, default is false.")]
        public bool autoDestroy = false;
        [Tooltip("modify self use modiefiers in stepModifiers when init, default is true")]
        public bool selfModify = true;
        [Tooltip("auto start when monobehaviour start, default is false.")]
        public bool startWhenStart = false;

        public List<StepModifier> stepModifiers = new List<StepModifier>();

        protected StepState _stepState = StepState.NotStarted;
        public StepState stepState
        {
            get
            {
                return _stepState;
            }
        }

        public UnityEvent<StepBase> OnStepFinished;

        public void AddFinishListner(UnityAction<StepBase> action)
        {
            if(OnStepFinished == null)
            {
                OnStepFinished = new UnityEvent<StepBase>();
            }
            OnStepFinished.AddListener(action);
        }


        #region Init
        protected virtual bool BeforeInit()
        {
            return true;
        }

        protected virtual void AfterInit()
        {

        }
        /// <summary>
        /// base initialize step, set parentStep and lastStep, can be override 
        /// </summary>
        public virtual void Init()
        {
            if (_stepState == StepState.Running)
            {
                return;
            }
            _stepState = StepState.NotStarted;
            if (BeforeInit())
            {
                if (selfModify)
                {
                    foreach (var modifier in stepModifiers)
                    {
                        modifier?.ModifyStep(this);
                    }
                }
                AfterInit();
            }
        }

        #endregion Init

        #region Start
        /// <summary>
        /// 一般用于检测是否可以开始，常在返回false的同时调用EndStep，即无法开启，直接结束
        /// </summary>
        /// <returns></returns>
        protected virtual bool BeforeStart()
        {
            return true;
        }

        protected virtual void AfterStart()
        {

        }
        /// <summary>
        /// start step, need to be override and called in subclass
        /// </summary>
        public virtual void StartStep()
        {

            if (_stepState != StepState.NotStarted)
            {
                return;
            }
            _stepState = StepState.Running;
            if (BeforeStart())
            {
                AfterStart();
            }
        }

        #endregion Start

        #region End
        protected virtual bool BeforeEnd()
        {
            return true;
        }

        protected virtual void AfterEnd()
        {
            if (autoDestroy)
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// set step end and invoke OnStepFinished event
        /// </summary>
        public virtual void EndStep()
        {
            if (_stepState == StepState.Finished)
            {
                return;
            }
            _stepState = StepState.Finished;
            if (BeforeEnd())
            {
                if (OnStepFinished != null)
                {
                    OnStepFinished.Invoke(this);
                }
                AfterEnd();
            }
        }

        #endregion

        #region modifier
        public virtual void AddModifire(StepModifier modifier)
        {
            if (modifier != null)
            {
                stepModifiers.Add(modifier);
            }
        }

        public virtual void removeModifire(int index)
        {
            if (index >= 0 && index < stepModifiers.Count)
            {
                stepModifiers.RemoveAt(index);
            }
        }

        #endregion modifier
        private void Start()
        {
            if (startWhenStart)
            {
                Init();
                StartStep();
            }
        }

        private void OnDestroy()
        {
            OnStepFinished?.RemoveAllListeners();
        }
    }
}