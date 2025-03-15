using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps
{

    // Base Step Class£¬Have a parentStep, and a OnStepFinished Event, will Invoke when Step Finished
    public class NonBlockStep : StepBase
    {
        /// <summary>
        /// the step need to run
        /// </summary>
        public StepBase runStep;

        StepBase generateStep;

        public UnityEvent<StepBase> innerStepFinished;

        public override void Init() 
        {
            base.Init();
            if (runStep != null)
            {
                generateStep = Instantiate(runStep);
                generateStep.AddFinishListner(InnerEnd);
                generateStep.Init();
            }
        }

        public override void StartStep()
        {
            base.StartStep();
            if (generateStep != null)
            {
                generateStep.autoDestroy = true;
                generateStep.StartStep();
            }
            EndStep();
        }

        protected virtual void InnerEnd(StepBase step)
        {
            innerStepFinished?.Invoke(step);
        }
    }
}