using UnityEngine;

namespace PigeonKingGames.Utils.Mouse
{
    public class FollowMouseOrthographic : MonoBehaviour
    {
        [SerializeField]
        private Camera camera;

        [SerializeField]
        private Transform target;

        [SerializeField]
        Vector2 offset;

        [SerializeField]
        bool followX = true;
        [SerializeField]
        bool followY = true;

        // Update is called once per frame
        void Update()
        {

            if (camera == null)
            {
                return;
            }
            // get mouse position in camera
            Vector3 mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
            // set z to 0
            var targetPos = target.position;
            if (followX)
            {
                targetPos.x = mousePos.x + offset.x;
            }
            if (followY)
            {
                targetPos.y = mousePos.y + offset.y;
            }
            // set position to target position
            target.position = targetPos;
        }
    }
}