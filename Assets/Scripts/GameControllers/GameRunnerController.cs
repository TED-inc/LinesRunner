#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;
using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    public sealed class GameRunnerController : MonoBehaviour
    {
        public static GameRunnerController instance;
        public IWorldController worldController { get; private set; }
        public IWorldPlatforms worldPlatforms { get; private set; }
        public IInputController inputController { get; private set; }
        public UnityEvent OnUpdate { get; private set; } = new UnityEvent();
        public UnityEvent OnFixedUpdate { get; private set; } = new UnityEvent();

        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private PlatformsHolderSO platformsHolderSO;
        [SerializeField]
        private ObstaclesHolderSO obstaclesHolderSO;

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
            inputController = new PCInputController();

            playerController.Init();

            worldController.LoadWorldUpTo(0f, GameConst.loadDistance);
        }

        private void FixedUpdate() =>
            OnFixedUpdate.Invoke();

        private void Update() =>
            OnUpdate.Invoke();
    }
}