using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class ObstaclesFactory : IObstaclesFactory
    {
        private ObstaclesHolderSO obstaclesHolderSO;
        private float obstacleDistance = GameConst.startObstacleDistance;

        public void SetObstacles(PlatformBase platformBase)
        {
            while (obstacleDistance < GameRunnerController.instance.worldPlatforms.generatedPlatformsLength)
            {
                ObstacleWeightHolder obstacleHolder = obstaclesHolderSO.GetNextByWeigth();

                GameObject instance = GameObject.Instantiate<GameObject>(
                    obstacleHolder.obstacle,
                    platformBase.transform);

                Pose pose = GameRunnerController.instance.worldController.ElevatePose(
                    obstacleDistance,
                    GetRandomElevation(obstacleHolder.linesCount));

                instance.transform.SetPositionAndRotation(pose.position, pose.rotation);

                obstacleDistance += Random.Range(GameConst.minObstacleDistance, GameConst.maxObstacleDistance);
            }

            float GetRandomElevation(int obstacleWidth)
            {
                float linePrecent = Mathf.InverseLerp(1f, GameConst.linesCount, obstacleWidth);
                float min = Mathf.Lerp(0f, 0.5f, linePrecent);
                float max = Mathf.Lerp(1f, 0.5f, linePrecent);
                int random = Random.Range(0, GameConst.linesCount - obstacleWidth + 1);

                return Mathf.Lerp(min, max, random / (GameConst.linesCount - 1f));
            }
        }

        public ObstaclesFactory(ObstaclesHolderSO obstaclesHolderSO)
        {
            this.obstaclesHolderSO = obstaclesHolderSO;
        }
    }
}