using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IPlatform
    {
        float length { get; }
        float offsetRotation { get; }
        Vector3 offset { get; }
        float totalOffsetRotation { get; }
        Vector3 totalOffset { get; }
        void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine);
        void SetWorldData(float totalOffsetRotation, Vector3 totalOffset);
    }
}