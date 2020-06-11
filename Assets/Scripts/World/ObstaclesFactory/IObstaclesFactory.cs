namespace TEDinc.LinesRunner
{
    /// <summary>
    /// factory of spawnable obstacles on platform
    /// </summary>
    public interface IObstaclesFactory
    {
        /// <summary>
        /// instantiate all obstacles in platform
        /// </summary>
        void SetObstacles(PlatformBase platformBase);
    }
}