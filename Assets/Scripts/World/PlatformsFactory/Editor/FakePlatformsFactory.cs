using UnityEngine;

namespace TEDinc.LinesRunner.Tests
{
    public sealed class FakePlatformsFactory : IPlatformsFactory
    {
        public float platformLength = 5f;
        public float platformRotation = 0f;

        public IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion)
        {
            FakePlatform fakePlatform;
            fakePlatform = new GameObject("fakePlatform", typeof(FakePlatform)).GetComponent<FakePlatform>();
            fakePlatform.ChangeLength(platformLength);
            fakePlatform.ChangeRotation(platformRotation);
            return fakePlatform;
        }
    }
}