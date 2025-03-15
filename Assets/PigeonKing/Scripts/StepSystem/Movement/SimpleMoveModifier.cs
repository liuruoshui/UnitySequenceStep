using PigeonKingGames.Steps;
using UnityEngine;
using UnityEngine.Events;

namespace PigeonKingGames.Steps
{
    public class SimpleMoveModifier : StepModifier
    {
        public Transform targetPosition;
        public Transform startPosition;
        public float moveSpeed = 0;
        public Transform moveTransform;


        public override StepBase ModifyStep(StepBase stepbase)
        {
            SimpleMove simpleMove = stepbase as SimpleMove;
            if (simpleMove != null)
            {
                if (targetPosition != null)
                {
                    simpleMove.endPoint = targetPosition.position;
                }
                if (startPosition != null)
                {
                    simpleMove.startPoint = startPosition.position;
                }
                if (moveSpeed != 0)
                {
                    simpleMove.speed = moveSpeed;
                }
                if (moveTransform != null)
                {
                    simpleMove.moveTransform = moveTransform;
                }
            }
            return stepbase;
        }
    }
}