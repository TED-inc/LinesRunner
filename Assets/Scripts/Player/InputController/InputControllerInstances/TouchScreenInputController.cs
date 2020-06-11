using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// android, ios, etc input controller
    /// </summary>
    public sealed class TouchScreenInputController : InputControllerBase
    {
        private Dictionary<int, Vector2> touchStartPositions = new Dictionary<int, Vector2>();

        protected override void OnUpdate()
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began)
                {
                    if (!touchStartPositions.ContainsKey(touch.fingerId))
                        touchStartPositions.Add(touch.fingerId, touch.position);
                }
                else if (touchStartPositions.ContainsKey(touch.fingerId))
                    if ((touch.position - touchStartPositions[touch.fingerId]).magnitude > GameConst.inputTouchMinLenthToMove)
                    {
                        ParseTouch(touch.position - touchStartPositions[touch.fingerId]);
                        touchStartPositions.Remove(touch.fingerId);
                    }
            }

            void ParseTouch(Vector2 delta)
            {

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
                {
                    if (delta.x > 0f)
                        OnMoveRight.Invoke();
                    else
                        OnMoveLeft.Invoke();
                }
                else if (delta.y > 0f)
                    OnJump.Invoke();

            }
        }
    }
}