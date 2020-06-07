namespace TEDinc.LinesRunner
{
    public sealed class FakePlatformsFactory : IPlatformsFactory
    {
        private int conter = 0;

        public IPlatform GetNextPlatform()
        {
            switch (conter++ % 3)
            {
                case 0:
                    return new FakePlatformA();
                case 1:
                    return new FakePlatformB();
                case 2:
                    return new FakePlatformC();
                default:
                    return null;
            }
        }
    }
}