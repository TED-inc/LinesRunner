using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlayerController : MonoBehaviour
    {
        private IPlayerMover playerMover;

        public void Init(IPlayerMover playerMover) =>
            this.playerMover = playerMover;

        public void Move(float distance)
        {
            Pose pose = playerMover.Move(distance);
            transform.position = pose.position;
            transform.rotation = pose.rotation;
        }
    }
}