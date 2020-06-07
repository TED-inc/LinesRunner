namespace TEDinc.LinesRunner
{
    public class PlatformsFactory : IPlatformsFactory
    {
        public IPlatform GetNextPlatform()
        {
            return new StraightPlatform();
        }
    }
}