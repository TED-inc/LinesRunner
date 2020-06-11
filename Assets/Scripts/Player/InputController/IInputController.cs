using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// call moving events by input
    /// </summary>
    public interface IInputController
    {
        UnityEvent OnMoveLeft { get; }
        UnityEvent OnMoveRight { get; }
        UnityEvent OnJump { get; }
    }
}