using PigeonKingGames.Steps;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    public class SimpleSpawn : StepBase
    {
        public GameObject prefab;
        public Transform spawnPoint;
        public Transform spawnParent;

        protected override void AfterInit()
        {
            if (spawnPoint == null)
            {
                spawnPoint = transform;
            }
        }
        protected override bool BeforeStart()
        {
            if (prefab == null)
            {
                Debug.LogError("Prefab is null");
                EndStep();
                return false;
            }
            return true;
        }

        protected override void AfterStart()
        {
            Spawn();
        }

        private void Spawn()
        {
            GameObject go = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            go.SetActive(true);

            if (spawnParent != null)
            {
                go.transform.SetParent(spawnParent);
            }
            EndStep();
        }
    }
}