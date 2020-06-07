namespace TEDinc.LinesRunner
{
    public abstract class FakePlatformBase : IPlatform
    {
        public abstract float length { get; protected set; }
    }

    public sealed class FakePlatformA : FakePlatformBase
    {
        public override float length { get; protected set; } = 5f;
    }

    public sealed class FakePlatformB : FakePlatformBase
    {
        public override float length { get; protected set; } = 10f;
    }
    public sealed class FakePlatformC : FakePlatformBase
    {
        public override float length { get; protected set; } = 13f;
    }
}