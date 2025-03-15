using System.Collections.Generic;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    // basic sequence step class, can add child steps, and run them in sequence
    [AddComponentMenu("Steps/SequenceStep")]
    public class SequenceStep : ControlStep
    {

        // loop means auto restart when end
        public bool isLoop = false;

        /// <summary>
        /// control play next step or end step or repeat
        /// </summary>
        /// <param name="finishedChild"></param>
        public override void ChildrenFinished(StepBase finishedChild)
        {
            finishedChild.OnStepFinished.RemoveListener(ChildrenFinished);
            var index = childrenSteps.IndexOf(finishedChild);
            if (index != childrenSteps.Count - 1)
            {
                StartChildStep(index + 1, finishedChild);
            }
            else
            {
                if (isLoop)
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
                    _stepState = StepState.NotStarted;
                    Init();
                    StartStep();
                }
                else
                {
                    EndStep();
                }
            }
        }

        protected override void LaunchChildrenStart()
        {
            StartChildStep(0, null);
        }
    }
}