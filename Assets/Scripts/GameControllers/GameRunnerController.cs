#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// main game controller. Holds all main data and other controllers
    /// </summary>
    public sealed class GameRunnerController : MonoBehaviour
    {
        public static GameRunnerController instance;
        public IWorldController worldController { get; private set; }
        public IWorldPlatforms worldPlatforms { get; private set; }
        public IInputController inputController { get; private set; }
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
            GameRunningDelegateController.instance.ResetDelegateLists();

            //initialize all
            inputController = GetInputController();
            worldPlatforms = new WorldPlatforms(
                new PlatformsFactory(platformsHolderSO),
                new ObstaclesFactory(obstaclesHolderSO));
            worldController = new WorldController(worldPlatforms);
            playerController.Init();
            worldController.LoadWorldUpTo(0f, GameConst.loadDistance);


            ///return input controller by platform
            IInputController GetInputController()
            {
#if UNITY_STANDALONE || UNITY_EDITOR
                return new KeyboardInputController();
#else
                return new TouchScreenInputController();
#endif
            }
        }
    }
}