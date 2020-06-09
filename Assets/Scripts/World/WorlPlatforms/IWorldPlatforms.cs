using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IWorldPlatforms
    {
        float generatedPlatformsLength { get; }
        float totalOffsetRotation { get; }
        Vector3 totalOffset { get; }
        List<PlatformHolder> platformHolders { get; }
        IPlatform GetPlatformAt(float distance);
        IPlatform GetPlatformAt(float distance, out float localDistance);
        void DestroyPlatformsBefore(float distance);
    }

    public sealed class PlatformHolder
    {
        public readonly float generatedLength;
        public readonly IPlatform platform;

        public PlatformHolder(float generatedLength, IPlatform platform)
        {
            this.generatedLength = generatedLength;
            this.platform = platform;
        }
    }
}