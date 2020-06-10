using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlatformsFactory : IPlatformsFactory
    {
        private PlatformsHolderSO platformsHolderSO;

        public IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion)
        {
            PlatformBase instance = GameObject.Instantiate<PlatformBase>(
                platformsHolderSO.GetNextByWeigth().platform,
                worldPosition,
                Quaternion.Euler(0f, worldRotataion, 0f),
                GetPlatformParent());

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
                    Debug.LogError($"[PLF] {nameof(GetPlatformParent)}: no instnce of GRC");
                return GameRunnerController.instance.transform;
#endif
            }
        }

        public PlatformsFactory(PlatformsHolderSO platformsHolderSO) =>
            this.platformsHolderSO = platformsHolderSO;
    }
}