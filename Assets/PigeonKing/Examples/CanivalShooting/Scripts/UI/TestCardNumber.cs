using UnityEngine;

namespace PigeonKingGames.CanivalShooting
{
    public class TestCardNumber : MonoBehaviour
    {
        public float intervalTime = 1.0f;
        public float currentTime = 0.0f;

        [SerializeField]
        NumberCardAnimator numberCardAnimator;
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            currentTime = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            currentTime += Time.deltaTime;
            if (currentTime > intervalTime) {
                currentTime = 0.0f;
                numberCardAnimator.SetNumber(Random.Range(0, 10));

            }

        }
    }
}