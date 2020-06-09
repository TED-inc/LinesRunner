using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlayerMover : IPlayerMover
    {
        public Pose Move(float distance)
        {
            Vector3 leftLine, rightLine;
            GameRunnerController.instance.worldController.ElevateLines(distance, out leftLine, out rightLine);
            GameRunnerController.instance.worldController.LoadWorldUpTo(distance, distance + GameConst.loadDistance);
            GameRunnerController.instance.worldController.HideWorldBefore(distance - GameConst.disableDistance);
            GameRunnerController.instance.worldPlatforms.DestroyPlatformsBefore(distance - GameConst.destroyDistance);

            return new Pose(leftLine, Quaternion.LookRotation(rightLine - leftLine, Vector3.up) * Quaternion.Euler(0f, -90f, 0f));
        }
    }
}