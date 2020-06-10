using UnityEngine;
using UnityEngine.Events;

namespace TEDinc.LinesRunner
{
    public sealed class PCInputController : InputControllerBase
    {
        protected override void OnUpdate()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnJump.Invoke();
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
                OnMoveLeft.Invoke();
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                OnMoveRight.Invoke();
        }
    }
}