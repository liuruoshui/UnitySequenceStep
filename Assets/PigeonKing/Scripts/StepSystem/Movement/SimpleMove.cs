using PigeonKingGames.Steps;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps
{
    public class SimpleMove : StepBase
    {
        // start point
        public Vector3 startPoint;
        // end point
        public Vector3 endPoint;
        // speed of movement
        public float speed = 1.0f;

        public Transform moveTransform;

        // Update is called once per frame
        void Update()
        {
            if (_stepState == StepState.Running)
            {
                float step = speed * Time.deltaTime;
                moveTransform.position = Vector3.MoveTowards(moveTransform.position, endPoint, step);
                if (moveTransform.position == endPoint)
                {
                    EndStep();
                }
            }
        }

        protected override bool BeforeStart()
        {
            if (startPoint != null) {
                moveTransform.position = startPoint;
            }
            return base.BeforeStart();
        }

        protected override void AfterInit()
        {
            if (moveTransform == null)
            {
                moveTransform = transform;
            }
            if (startPoint == null)
            {
                startPoint = moveTransform.position;
            }
            if (endPoint == null)
            {
                endPoint = moveTransform.position;
            }

        }
    }
}