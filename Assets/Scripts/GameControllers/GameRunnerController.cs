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
        [SerializeField]
        private ObstaclesHolderSO obstaclesHolderSO;

        ///<summary>temporary variable</summary>
        public float testDistance;
        ///<summary>temporary variable</summary>
        [Range(0f, 1f)]
        public float testWidthElevation;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GRC] Only one instance must be in game!");

            worldPlatforms = new WorldPlatforms(
                new PlatformsFactory(platformsHolderSO),
                new ObstaclesFactory(obstaclesHolderSO));
            worldController = new WorldController(worldPlatforms);

            playerController.Init(new PlayerMover());
        }

        private void FixedUpdate()
        {
            worldController.LoadWorldUpTo(0f, GameConst.loadDistance);
            playerController.Move(testDistance, testWidthElevation);
        }
    }
}