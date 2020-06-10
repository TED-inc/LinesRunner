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
        public UnityEvent OnUpdateWhileRunning { get; private set; } = new UnityEvent();
        public UnityEvent OnFixedUpdateWhileRunning { get; private set; } = new UnityEvent();

        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private PlatformsHolderSO platformsHolderSO;
        [SerializeField]
        private ObstaclesHolderSO obstaclesHolderSO;

        public bool gameRun = true;

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
            inputController = new TouchScreenInputController();

            playerController.Init();
            worldController.LoadWorldUpTo(0f, GameConst.loadDistance);
        }

        private void FixedUpdate()
        {
            if (gameRun)
                OnFixedUpdateWhileRunning.Invoke();
        }

        private void Update()
        {
            if (gameRun)
                OnUpdateWhileRunning.Invoke();
        }
    }
}