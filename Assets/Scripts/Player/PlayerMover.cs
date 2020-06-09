using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlayerMover : IPlayerMover
    {
        public Pose Move(float distance, float widthElevation)
        {
            Vector3 leftLine, rightLine;
            GameRunnerController.instance.worldController.ElevateLines(distance, out leftLine, out rightLine);
            GameRunnerController.instance.worldController.LoadWorldUpTo(distance, distance + GameConst.loadDistance);
            GameRunnerController.instance.worldController.HideWorldBefore(distance - GameConst.disableDistance);
            GameRunnerController.instance.worldPlatforms.DestroyPlatformsBefore(distance - GameConst.destroyDistance);

            return new Pose(
                Vector3.Lerp(leftLine, rightLine, widthElevation),
                Quaternion.LookRotation(rightLine - leftLine, Vector3.up) * Quaternion.Euler(0f, 180f, 0f));
        }
    }
}