using UnityEngine;
using UnityEngine.TestTools.Utils;
using UnityEditor;
using NUnit.Framework;

namespace TEDinc.LinesRunner.Tests
{
    public sealed class TestWorldController
    {
        [Test]
        public void LineElevationOffset()
        {
            Vector3 leftLine, rightLine;
            FakePlatformsFactory fakePlatformsFactory = new FakePlatformsFactory();
            IWorldController worldControler = new WorldController(new WorldPlatforms(fakePlatformsFactory));
            float distance;

            fakePlatformsFactory.platformLength = 5f;

            distance = 5f;
            CheckOffset();
            distance = 5.01f;
            CheckOffset();
            distance = 4.99f;
            CheckOffset();
            distance = 1.5f;
            CheckOffset();
            distance = 12f;
            CheckOffset();
            distance = 230f;
            CheckOffset();


            void CheckOffset()
            {
                worldControler.ElevateLines(distance, out leftLine, out rightLine);
                Assert.That(leftLine, Is.EqualTo(new Vector3(distance, 0f, 1f)).Using(Vector3EqualityComparer.Instance));
                Assert.That(rightLine, Is.EqualTo(new Vector3(distance, 0f, -1f)).Using(Vector3EqualityComparer.Instance));
            }
        }

        [Test]
        public void LineElevationRotation()
        {
            Vector3 leftLine, rightLine;
            FakePlatformsFactory fakePlatformsFactory = new FakePlatformsFactory();
            IWorldController worldControler = new WorldController(new WorldPlatforms(fakePlatformsFactory));

            fakePlatformsFactory.platformLength = 5f;
            fakePlatformsFactory.platformRotation = 90f;

            worldControler.ElevateLines(2f, out leftLine, out rightLine);
            Assert.That(leftLine, Is.EqualTo(new Vector3(2f, 0f, 1f)).Using(Vector3EqualityComparer.Instance));
            Assert.That(rightLine, Is.EqualTo(new Vector3(2f, 0f, -1f)).Using(Vector3EqualityComparer.Instance));
            
            worldControler.ElevateLines(4.99f, out leftLine, out rightLine);
            Assert.That(leftLine, Is.EqualTo(new Vector3(4.99f, 0f, 1f)).Using(Vector3EqualityComparer.Instance));
            Assert.That(rightLine, Is.EqualTo(new Vector3(4.99f, 0f, -1f)).Using(Vector3EqualityComparer.Instance));

            worldControler.ElevateLines(7.5f, out leftLine, out rightLine);
            Assert.That(leftLine, Is.EqualTo(new Vector3(6f, 0f, -2.5f)).Using(Vector3EqualityComparer.Instance));
            Assert.That(rightLine, Is.EqualTo(new Vector3(4f, 0f, -2.5f)).Using(Vector3EqualityComparer.Instance));

            worldControler = new WorldController(new WorldPlatforms(fakePlatformsFactory));
            fakePlatformsFactory.platformRotation = 45f;
            
            worldControler.ElevateLines(7.5f, out leftLine, out rightLine);
            Assert.That(leftLine, Is.EqualTo(new Vector3(7.474874f, 0f, -1.06066f)).Using(Vector3EqualityComparer.Instance));
            Assert.That(rightLine, Is.EqualTo(new Vector3(6.06066f, 0f, -2.474874f)).Using(Vector3EqualityComparer.Instance));

            worldControler.ElevateLines(12.5f, out leftLine, out rightLine);
            Assert.That(leftLine, Is.EqualTo(new Vector3(9.535534f, 0f, -6.035534f)).Using(Vector3EqualityComparer.Instance));
            Assert.That(rightLine, Is.EqualTo(new Vector3(7.535534f, 0f, -6.035534f)).Using(Vector3EqualityComparer.Instance));
        }

        [Test]
        public void LineElevationReal()
        {
            Vector3 leftLineA, rightLineA, leftLineB, rightLineB;
            IWorldController worldControler = new WorldController(new WorldPlatforms(new PlatformsFactory(
                AssetDatabase.LoadAssetAtPath<PlatformsHolderSO>(
                    AssetDatabase.GUIDToAssetPath(
                        AssetDatabase.FindAssets("t:" + nameof(PlatformsHolderSO))[0]))
                )));

            worldControler.ElevateLines(49.9999f, out leftLineA, out rightLineA);
            worldControler.ElevateLines(50.0001f, out leftLineB, out rightLineB);
            Assert.That(leftLineA, Is.EqualTo(leftLineB).Using(Vector3EqualityComparer.Instance));

            worldControler.ElevateLines(74.9999f, out leftLineA, out rightLineA);
            worldControler.ElevateLines(75.0001f, out leftLineB, out rightLineB);
            Assert.That(leftLineA, Is.EqualTo(leftLineB).Using(Vector3EqualityComparer.Instance));

            worldControler.ElevateLines(149.9999f, out leftLineA, out rightLineA);
            worldControler.ElevateLines(150.0001f, out leftLineB, out rightLineB);
            Assert.That(leftLineA, Is.EqualTo(leftLineB).Using(Vector3EqualityComparer.Instance));
        }
    }
}