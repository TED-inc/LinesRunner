#if UNITY_EDITOR
namespace TEDinc.LinesRunner.Tests
{
    public sealed class FakePlatform : PlatformBase
    {
        public void ChangeLength(float length)
        {
            this.length = length;
        }
        public void ChangeRotation(float offsetRotation)
        {
            this.offsetRotation = offsetRotation;
        }
    }
}
#endif