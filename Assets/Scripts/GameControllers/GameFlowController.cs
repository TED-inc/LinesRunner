#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public sealed class GameFlowController : MonoBehaviour
    {
        public static GameFlowController instance;
        public bool isGameRunning { get; private set; }
        private bool playerHited;

        [SerializeField]
        private GameObject startOverlay;
        [SerializeField]
        private GameObject runOverlay;
        [SerializeField]
        private GameObject pauseOverlay;
        [SerializeField]
        private GameObject playerHitOverlay;


        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GFC] Only one instance must be in game!");
        }

        private void Start()
        {
            ShowStartScreen();
        }


        public void HitPlayer()
        {
            ShowOverlay(Overlay.playerHit);
            playerHited = true;
            isGameRunning = false;
        }

        public void PauseGame()
        {
            if (playerHited)
                return;

            ShowOverlay(Overlay.pause);
            isGameRunning = false;
        }
         
        public void ResumeGame()
        {
            if (playerHited)
                return;

            ShowOverlay(Overlay.run);
            isGameRunning = true;
        }

        public void ShowStartScreen()
        {
            ShowOverlay(Overlay.start);
            playerHited = false;
            isGameRunning = false;
            GameRunnerController.instance.RestatrGameController();
            GameRunnerController.instance.OnUpdateWhileRunning.Invoke();
            GameRunnerController.instance.OnFixedUpdateWhileRunning.Invoke();
        }

        public void RestartGame()
        {
            ShowOverlay(Overlay.run);
            playerHited = false;
            isGameRunning = true;
            GameRunnerController.instance.RestatrGameController();
        }


        private void ShowOverlay(Overlay overlay)
        {
            startOverlay.SetActive(false);
            runOverlay.SetActive(false);
            pauseOverlay.SetActive(false);
            playerHitOverlay.SetActive(false);

            switch (overlay)
            {
                case Overlay.none:
                    break;
                case Overlay.start:
                    startOverlay.SetActive(true);
                    break;
                case Overlay.run:
                    runOverlay.SetActive(true);
                    break;
                case Overlay.pause:
                    pauseOverlay.SetActive(true);
                    break;
                case Overlay.playerHit:
                    playerHitOverlay.SetActive(true);
                    break;
                default:
                    break;
            }
        }

        internal enum Overlay
        {
            none,
            start,
            run,
            pause,
            playerHit
        }
    }
}