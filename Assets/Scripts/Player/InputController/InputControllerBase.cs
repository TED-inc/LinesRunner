using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    public abstract class InputControllerBase : IInputController
    {
        public UnityEvent OnMoveLeft { get; private set; } = new UnityEvent();

        public UnityEvent OnMoveRight { get; private set; } = new UnityEvent();

        public UnityEvent OnJump { get; private set; } = new UnityEvent();

        protected abstract void OnUpdate();

        public InputControllerBase() =>
            GameRunnerController.instance.OnUpdate.AddListener(OnUpdate);
    }
}