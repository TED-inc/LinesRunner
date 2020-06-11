using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// factory of platforms
    /// </summary>
    public interface IPlatformsFactory
    {
        /// <summary>
        /// ganearte platform and instantiate it if it posible
        /// </summary>
        IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion);
    }
}