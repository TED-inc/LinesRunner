using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlayerMover : IPlayerMover
    {
        public Pose Move(float distance, float widthElevation)
        {
            GameRunnerController.instance.worldController.LoadWorldUpTo(distance, distance + GameConst.loadDistance);
            GameRunnerController.instance.worldController.HideWorldBefore(distance - GameConst.disableDistance);
            GameRunnerController.instance.worldPlatforms.DestroyPlatformsBefore(distance - GameConst.destroyDistance);

            return GameRunnerController.instance.worldController.ElevatePose(distance, widthElevation);
        }
    }
}