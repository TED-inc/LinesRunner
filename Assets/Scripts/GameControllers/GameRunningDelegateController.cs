using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// calls delegates for subscribed non monobehaviours classes
    /// </summary>
    public sealed class GameRunningDelegateController : MonoBehaviour
    {
        public static GameRunningDelegateController instance;
        public List<Del> onFixedUpdateWhileRunning { get; private set; }
        public List<Del> onUpdateWhileRunning { get; private set; }

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GRD] Only one instance must be in game!");
        }

        private void FixedUpdate()
        {
            if (GameFlowController.instance.isGameRunning)
                FixedUpdateWhileRunningInvoke();
        }

        public void FixedUpdateWhileRunningInvoke()
        {
            foreach (Del deleg in onFixedUpdateWhileRunning)
                deleg.DynamicInvoke();
        }

        private void Update()
        {
            if (GameFlowController.instance.isGameRunning)
                UpdateWhileRunningInvoke();
        }

        public void UpdateWhileRunningInvoke()
        {
            foreach (Del deleg in onUpdateWhileRunning)
                deleg.DynamicInvoke();
        }

        public void ResetDelegateLists()
        {
            onFixedUpdateWhileRunning = new List<Del>();
            onUpdateWhileRunning = new List<Del>();
        }

    }

    public delegate void Del();
}