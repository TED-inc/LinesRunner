using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IPlatform
    {
        float length { get; }
        /// <summary>
        /// local Y rotation for next generated platform
        /// </summary>
        float offsetRotation { get; }
        /// <summary>
        /// ending vector of platform
        /// </summary>
        Vector3 offset { get; }
        /// <summary>
        /// gloabal Y rotation for next generated platform
        /// </summary>
        float totalOffsetRotation { get; }
        /// <summary>
        /// gloabal ending vector of platform
        /// </summary>
        Vector3 totalOffset { get; }
        /// <summary>
        /// calcualte elevation by distance
        /// </summary>
        /// <param name="localLeftLine">position on leftest line(min)</param>
        /// <param name="localRightLine">position on rightest line(max)</param>
        void ElevateLines(float localDistance, out Vector3 localLeftLine, out Vector3 localRightLine);
        void SetWorldData(float totalOffsetRotation, Vector3 totalOffset);
    }
}