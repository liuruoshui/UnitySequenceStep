using System.Collections.Generic;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    // basic any step, start all children when step start, if any of the child steps finished, this step will finish
    [AddComponentMenu("Steps/ImportantStep")]
    public class ImportantStep : AnyStep
    {

        public int importantIndex = 0;

        /// <summary>
        /// control play next step or end step or repeat
        /// </summary>
        /// <param name="finishedChild"></param>
        public override void ChildrenFinished(StepBase finishedChild)
        {
            if(importantIndex < 0 || importantIndex >= childrenSteps.Count)
            {
                Debug.LogError("ImportantIndex is out of range");
                EndStep();
            }
            var index = childrenSteps.IndexOf(finishedChild);
            if (index == importantIndex)
            {
                EndStep();
            }
        }
    }
}