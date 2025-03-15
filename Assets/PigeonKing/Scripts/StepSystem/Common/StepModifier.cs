using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps
{
    /// <summary>
    /// Setting specific step properties
    /// </summary>
    public class StepModifier : MonoBehaviour
    {
        public virtual StepBase ModifyStep(StepBase stepbase)
        {
            return stepbase;
        }
    }
}