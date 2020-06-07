using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class PlatformsFactory : IPlatformsFactory
    {
        public IPlatform GetNextPlatform()
        {
            return new GameObject("platform", typeof(StraightPlatform)).GetComponent<StraightPlatform>();
        }
    }
}