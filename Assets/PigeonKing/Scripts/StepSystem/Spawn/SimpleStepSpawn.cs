using PigeonKingGames.Steps;
using UnityEngine;

namespace PigeonKingGames.Steps
{
    public class SimpleStepSpawn : StepBase
    {
        public GameObject prefab;
        public Transform spawnPoint;
        public Transform spawnParent;
        public bool waitForStepEnd;
        StepBase generatedStep;

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

        protected override bool BeforeEnd()
        {
            ClearOneStep(generatedStep);
            return base.BeforeEnd();
        }

        private void Spawn()
        {
            GameObject go = Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
            go.SetActive(true);

            if (spawnParent != null)
            {
                go.transform.SetParent(spawnParent);
            }
            
            generatedStep = go.GetComponent<StepBase>();
            if (generatedStep != null)
            {
                generatedStep.Init();
                if (waitForStepEnd)
                {
                    generatedStep.AddFinishListner(SpawnedStepEnd);
                    generatedStep.StartStep();
                }
                else
                {
                    generatedStep.StartStep();
                    EndStep();
                }
            }
            else
            {
                Debug.LogWarning("Spawned object does not have a StepBase component");
                EndStep();
            }
        }

        private void SpawnedStepEnd(StepBase step)
        {
            StepBase.ClearOneStep(generatedStep);
            EndStep();
        }
    }
}