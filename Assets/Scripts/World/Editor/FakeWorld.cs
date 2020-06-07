namespace TEDinc.LinesRunner
{
    public sealed class FakeWorld : IWorld
    {
        public float generatedPlatformsLength { get; private set; } = float.MinValue;
        public IPlatform GetPlatformAt(float distance)
        {
            return new StraightPlatform();
        }
    }
}