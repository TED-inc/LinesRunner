using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class ObstaclesFactory : IObstaclesFactory
    {
        private ObstaclesHolderSO obstaclesHolderSO;

        public void SetObstacles(PlatformBase platformBase)
        {
            GameObject instance = GameObject.Instantiate<GameObject>(
                obstaclesHolderSO.GetNextByWeigth(),
                platformBase.transform.position,
                platformBase.transform.rotation,
                platformBase.transform);
        }

        public ObstaclesFactory(ObstaclesHolderSO obstaclesHolderSO)
        {
            this.obstaclesHolderSO = obstaclesHolderSO;
        }
    }
}