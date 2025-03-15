using System.Collections.Generic;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    public class MultiRunStep : StepBase
    {
        [SerializeField]
        StepBase needRunStep;

        StepBase innerStep;

        [SerializeField]
        int runTimes = 0;
        [SerializeField]
        int maxRunTimes = 1;

        [Tooltip("auto destroy all set sequence steps when destroy this")]
        // auto destroy all setSequenceSteps
        public bool DestroyWhenDestroy = false;

        [Tooltip("if true, will initialize actual sequence steps instead of source steps, for same step used multi times, don't change it when running, may cause bug")]
        public bool childStartWithNewObject = true;


        protected override bool BeforeInit()
        {
            if(needRunStep == null)
            {
                return false;
            }
            if(maxRunTimes == 0)
            {
                return false;
            }
            if (childStartWithNewObject)
            {
                innerStep = Instantiate(needRunStep);
                innerStep.transform.SetParent(transform);
            }
            else
            {
                innerStep = needRunStep;
            }
            return true;
        }

        protected override void AfterInit()
        {
            runTimes = 0;
        }
        protected override bool BeforeStart()
        {
            if(needRunStep == null || maxRunTimes == 0)
            {
                Debug.LogError("RunSeverialTimesStep need a step to run");
                EndStep();
                return false;
            }
            else
            {
                innerStep.gameObject.SetActive(true);
                innerStep.AddFinishListner(OnInnerStepFinished);
                innerStep.Init();
                return true;
            }
        }

        protected override void AfterStart()
        {
            innerStep.StartStep();
        }

        void OnInnerStepFinished(StepBase step)
        {
            runTimes++;
            if(runTimes >= maxRunTimes)
            {
                EndStep();
            }
            else
            {
                innerStep.Init();
                innerStep.StartStep();
            }
        }

        protected override bool BeforeEnd()
        {
            if(childStartWithNewObject)
            {
                ClearOneStep(innerStep);
            }
            else
            {
                TerminateOneStep(innerStep);
            }
            return base.BeforeEnd();
        }


        private void OnDestroy()
        {
            ClearOneStep(innerStep);
            if (DestroyWhenDestroy)
            {
                ClearOneStep(needRunStep);
            }
            else
            {
                TerminateOneStep(needRunStep);
            }
        }
    }
}