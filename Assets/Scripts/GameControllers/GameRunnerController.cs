#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class GameRunnerController : MonoBehaviour
    {
        public static GameRunnerController instance;
        public IWorldController worldController { get; private set; }
        public IWorldPlatforms worldPlatforms { get; private set; }

        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private PlatformsHolderSO platformsHolderSO;
        public float testDistance;

        private void Awake()
        {
            worldPlatforms = new WorldPlatforms(new PlatformsFactory(platformsHolderSO));
            worldController = new WorldController(worldPlatforms);

            playerController.Init(new PlayerMover());

            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GRC] Only one instance must be in game!");
        }

        private void Start() =>
            worldController.LoadWorldUpTo(GameConst.loadDistance);

        private void Update() =>
            playerController.Move(testDistance);
    }
}