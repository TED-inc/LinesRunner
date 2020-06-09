using NUnit.Framework;

namespace TEDinc.LinesRunner.Tests
{
    public sealed class TestWorldPlatforms
    {
        [Test]
        public void PlacingTest()
        {
            FakePlatformsFactory fakePlatformsFactory = new FakePlatformsFactory();
            WorldPlatforms world = new WorldPlatforms(fakePlatformsFactory, new FakeObstacleFactory());

            fakePlatformsFactory.platformLength = 1f;
            Assert.AreEqual(1f, world.GetPlatformAt(1f - 0.0001f).length);
            fakePlatformsFactory.platformLength = 2f;
            Assert.AreEqual(2f, world.GetPlatformAt(1f + 0.0001f).length);
            Assert.AreEqual(2f, world.GetPlatformAt(1f + 2f - 0.0001f).length);
            fakePlatformsFactory.platformLength = 3f;
            Assert.AreEqual(3f, world.GetPlatformAt(1f + 2f + 0.0001f).length);
            Assert.AreEqual(3f, world.GetPlatformAt(1f + 2f + 3f).length);
        }

        [Test]
        public void CheckSpawning()
        {
            IWorldPlatforms world = new WorldPlatforms(new FakePlatformsFactory(), new FakeObstacleFactory());
            Assert.AreEqual("fakePlatform", (world.GetPlatformAt(1f) as PlatformBase).name);
        }
    }
}