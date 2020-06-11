using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// implementation of IPlayerMover:
    /// controls player horizontal movement
    /// </summary>
    public sealed class PlayerMover : IPlayerMover
    {
        private float time = 0f;
        private float widthElevation;
        private float lineChangeTime;
        private int targetLine;

        public Pose Move()
        {
            float distance = 
                GameConst.startPlayerSpeed * time
                + 0.5f * GameConst.playerAcceleration * time * time
                + GameConst.startPlayerDistance;

            PlayerPrefs.SetInt(nameof(GameConst.PlayerPrefs.distance), (int)(distance - GameConst.startPlayerDistance));

            GameRunnerController.instance.worldController.LoadWorldUpTo(distance, distance + GameConst.loadDistance);
            GameRunnerController.instance.worldController.HideWorldBefore(distance - GameConst.disableDistance);
            GameRunnerController.instance.worldPlatforms.DestroyPlatformsBefore(distance - GameConst.destroyDistance);

            widthElevation = Mathf.Lerp(
                widthElevation, 
                targetLine / (GameConst.linesCount - 1f),
                Mathf.InverseLerp(0f, GameConst.playerChangeLineDyration, time - lineChangeTime));

            time += Time.deltaTime;

            return GameRunnerController.instance.worldController.ElevatePose(distance, widthElevation);
        }

        public PlayerMover()
        {
            GameRunnerController.instance.inputController.OnMoveLeft.AddListener(MoveLeft);
            GameRunnerController.instance.inputController.OnMoveRight.AddListener(MoveRight);

            void MoveLeft() =>
                ChangeLIne(-1);
            void MoveRight() =>
               ChangeLIne(1);

            void ChangeLIne(int change)
            {
                targetLine = Mathf.Clamp(targetLine + change, 0, GameConst.linesCount);
                lineChangeTime = time;
            }
        }
    }
}