using NUnit.Framework;

namespace TEDinc.LinesRunner
{
    public sealed class TestWorld
    {
        [Test]
        public void PlacingTest()
        {
            World world = new World(new FakePlatformsFactory());

            Assert.AreEqual(
                new FakePlatformA().length,
                world.GetPlatformAt(new FakePlatformA().length - 0.0001f).length);
            Assert.AreEqual(
                new FakePlatformB().length,
                world.GetPlatformAt(new FakePlatformA().length + 0.0001f).length);
            Assert.AreEqual(
                new FakePlatformB().length,
                world.GetPlatformAt(new FakePlatformA().length + new FakePlatformB().length - 0.0001f).length);
            Assert.AreEqual(
                new FakePlatformC().length,
                world.GetPlatformAt(new FakePlatformA().length + new FakePlatformB().length + 0.0001f).length);
            Assert.AreEqual(
                new FakePlatformC().length,
                world.GetPlatformAt(new FakePlatformA().length + new FakePlatformB().length + new FakePlatformC().length).length);
        }
    }
}