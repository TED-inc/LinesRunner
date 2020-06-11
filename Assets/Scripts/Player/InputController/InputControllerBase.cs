using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// base implemenation of IInputController:
    /// call moving events by input
    /// </summary>
    public abstract class InputControllerBase : IInputController
    {
        public UnityEvent OnMoveLeft { get; private set; } = new UnityEvent();

        public UnityEvent OnMoveRight { get; private set; } = new UnityEvent();

        public UnityEvent OnJump { get; private set; } = new UnityEvent();

        /// <summary>
        /// calls on update while game running
        /// </summary>
        protected abstract void OnUpdate();

        public InputControllerBase() =>
            GameRunningDelegateController.instance.onUpdateWhileRunning.Add(OnUpdate);
    }
}