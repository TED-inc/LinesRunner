using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// implementation of IPlatformsFactory:
    /// factory of platforms
    /// </summary>
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
                    GameRunnerController.instance.platformsHolderObject;
#else
                if (GameRunnerController.instance == null)
                    Debug.LogError($"[PLF] {nameof(GetPlatformParent)}: no instnce of GRC");
                return GameRunnerController.instance.platformsHolderObject;
#endif
            }
        }

        public PlatformsFactory(PlatformsHolderSO platformsHolderSO) =>
            this.platformsHolderSO = platformsHolderSO;
    }
}