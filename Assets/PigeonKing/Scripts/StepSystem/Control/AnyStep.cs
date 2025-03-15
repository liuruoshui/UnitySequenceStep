using System.Collections.Generic;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    // basic any step, start all children when step start, if any of the child steps finished, this step will finish
    [AddComponentMenu("Steps/AnyStep")]
    public class AnyStep : ControlStep
    {
        /// <summary>
        /// control play next step or end step or repeat
        /// </summary>
        /// <param name="finishedChild"></param>
        public override void ChildrenFinished(StepBase finishedChild)
        {
            EndStep();
        }


        protected override void LaunchChildrenStart()
        {
            for (var i = 0; i < childrenSteps.Count; i++)
            {
                StartChildStep(i, null);
            }
        }
    }
}