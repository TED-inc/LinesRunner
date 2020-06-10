using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    public interface IInputController
    {
        UnityEvent OnMoveLeft { get; }
        UnityEvent OnMoveRight { get; }
        UnityEvent OnJump { get; }
    }
}