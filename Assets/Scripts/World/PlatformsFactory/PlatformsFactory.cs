using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlatformsFactory : IPlatformsFactory
    {
        private PlatformsHolderSO platformsHolderSO;

        public IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion)
        {
            
            int index = GetRandomPlatformIndex();

            return GameObject.Instantiate<PlatformBase>(
                platformsHolderSO.platforms[index].platform,
                worldPosition,
                Quaternion.Euler(0f, worldRotataion, 0f),
                GameRunnerController.instance == null ?
                    null :
                    GameRunnerController.instance.transform);



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
        }

        public PlatformsFactory(PlatformsHolderSO platformsHolderSO) =>
            this.platformsHolderSO = platformsHolderSO;
    }
}