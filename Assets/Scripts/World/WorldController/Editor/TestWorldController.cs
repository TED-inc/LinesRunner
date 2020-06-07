using UnityEngine;
using UnityEngine.TestTools.Utils;
using NUnit.Framework;

namespace TEDinc.LinesRunner
{
    public sealed class TestWorldController
    {
        [Test]
        public void LineElevation()
        {
            Vector3 leftLine, rightLine;
            IWorldController worldControler = new WorldController(new FakeWorld());
            float distance = 5f;

            worldControler.ElevateLines(distance, out leftLine, out rightLine);
            Assert.That(leftLine, Is.EqualTo(new Vector3(distance, 0f, -1f)).Using(Vector3EqualityComparer.Instance));
            Assert.That(rightLine, Is.EqualTo(new Vector3(distance, 0f, 1f)).Using(Vector3EqualityComparer.Instance));
        }
    }
}