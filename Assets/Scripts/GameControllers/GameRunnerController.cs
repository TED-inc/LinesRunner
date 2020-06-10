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
        public Transform platformsHolderObject { get => _platformsHolderObject; }

        [SerializeField]
        private PlayerController playerController;
        [SerializeField]
        private PlatformsHolderSO platformsHolderSO;
        [SerializeField]
        private ObstaclesHolderSO obstaclesHolderSO;
        [SerializeField]
        private Transform _platformsHolderObject;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GRC] Only one instance must be in game!");
        }

        [ContextMenu("RestatrGame")]
        public void RestatrGameController()
        {
            //clear game on start
            foreach (Transform child in platformsHolderObject)
                GameObject.Destroy(child.gameObject);
            playerController.transform.position = Vector3.zero;
            OnUpdateWhileRunning.RemoveAllListeners();
            OnFixedUpdateWhileRunning.RemoveAllListeners();

            //initialize all
            worldPlatforms = new WorldPlatforms(
                new PlatformsFactory(platformsHolderSO),
                new ObstaclesFactory(obstaclesHolderSO));
            worldController = new WorldController(worldPlatforms);
            inputController = GetInputController();
            playerController.Init();
            worldController.LoadWorldUpTo(0f, GameConst.loadDistance);

            IInputController GetInputController()
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                return new KeyboardInputController();
#else
                return new TouchScreenInputController();
#endif
            }
        }

        private void FixedUpdate()
        {
            if (GameFlowController.instance.isGameRunning)
                OnFixedUpdateWhileRunning.Invoke();
        }

        private void Update()
        {
            if (GameFlowController.instance.isGameRunning)
                OnUpdateWhileRunning.Invoke();
        }
    }
}