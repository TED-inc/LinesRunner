#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class GameRunnerController : MonoBehaviour
    {
        public static GameRunnerController instance;

        [SerializeField]
        private PlatformsHolderSO platformsHolderSO;
        private IWorldController worldController;
        private IWorldPlatforms worldPlatforms;
        private IPlatformsFactory platformsFactory;


        private void Start()
        {
            platformsFactory = new PlatformsFactory(platformsHolderSO);
            worldPlatforms = new WorldPlatforms(platformsFactory);
            worldController = new WorldController(worldPlatforms);

            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GRC] Only one instance must be in game!");

            worldController.LaodWorldUpTo(GameConst.loadDistance);
        }
    }
}