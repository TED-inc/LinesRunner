using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IWorldPlatforms
    {
        float generatedPlatformsLength { get; }
        float totalOffsetRotation { get; }
        Vector3 totalOffset { get; }
        IPlatform GetPlatformAt(float distance);
        IPlatform GetPlatformAt(float distance, out float localDistance);
        float GetTotalOffsetRotationAt(float distance);
        Vector3 GetTotalOffsetAt(float distance);
    }
}