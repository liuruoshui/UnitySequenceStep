using PigeonKingGames.Steps;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    public class TimerStep : StepBase
    {
        public float time = 1.0f;
        private float currentTime = 0.0f;

        protected override void AfterStart()
        {
            currentTime = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            if (_stepState == StepState.Running)
            {
                currentTime += Time.deltaTime;
                if (currentTime >= time)
                {
                    EndStep();
                }
            }
        }
    }
}