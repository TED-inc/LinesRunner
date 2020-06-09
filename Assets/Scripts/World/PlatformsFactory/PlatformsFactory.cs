using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlatformsFactory : IPlatformsFactory
    {
        private PlatformsHolderSO platformsHolderSO;
        private IObstaclesFactory obstaclesFactory;

        public IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion)
        {
            PlatformBase instance = GameObject.Instantiate<PlatformBase>(
                platformsHolderSO.GetNextByWeigth(),
                worldPosition,
                Quaternion.Euler(0f, worldRotataion, 0f),
                GetPlatformParent());

            obstaclesFactory.SetObstacles(instance);

            return instance;


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

        public PlatformsFactory(PlatformsHolderSO platformsHolderSO, IObstaclesFactory obstaclesFactory)
        {
            this.platformsHolderSO = platformsHolderSO;
            this.obstaclesFactory = obstaclesFactory;
        }
    }
}