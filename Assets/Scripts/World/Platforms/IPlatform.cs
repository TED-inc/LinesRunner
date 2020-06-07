using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IPlatform
    {
        float length { get; }
        float offsetRotation { get; }
        Vector3 offset { get; }
        void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine);
    }
}