using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlatformsFactory : IPlatformsFactory
    {
        private PlatformsHolderSO platformsHolderSO;

        public IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion)
        {
            int index = GetRandomPlatformIndex();
            Transform parent = GetPlatformParent();
            PlatformBase platformBase = platformsHolderSO.platforms[index].platform;

            return GameObject.Instantiate<PlatformBase>(
                platformBase,
                worldPosition,
                Quaternion.Euler(0f, worldRotataion, 0f),
                parent);



            int GetRandomPlatformIndex()
            {
                float totalWeight = 0f, randomWeight;

                foreach (PlatformsHolderSO.PlatformWithWeight item in platformsHolderSO.platforms)
                    totalWeight += item.weight;

                randomWeight = Random.Range(0f, totalWeight);

                for (int i = 0; i < platformsHolderSO.platforms.Length; i++)
                    if (platformsHolderSO.platforms[i].weight > randomWeight)
                        return i;
                    else
                        randomWeight -= platformsHolderSO.platforms[i].weight;

                Debug.LogError($"[PLF] {nameof(GetRandomPlatformIndex)}: some logic error");
                return -1;
            }

            Transform GetPlatformParent()
            {
#if UNITY_EDITOR
                //null only for tests
                return GameRunnerController.instance == null ?
                    null :
                    GameRunnerController.instance.transform;
#else
                if (GameRunnerController.instance == null)
                    Debug.LogError($"[PLF] {nameof(GetRandomPlatformIndex)}: no instnce of GRC");
                return GameRunnerController.instance.transform;
#endif
            }
        }

        public PlatformsFactory(PlatformsHolderSO platformsHolderSO) =>
            this.platformsHolderSO = platformsHolderSO;
    }
}