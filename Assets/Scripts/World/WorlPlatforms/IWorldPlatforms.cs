using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// keeps data of platforms and save getting of them
    /// </summary>
    public interface IWorldPlatforms
    {
        /// <summary>
        /// summ distance of all platforms
        /// </summary>
        float generatedPlatformsLength { get; }
        /// <summary>
        /// Y rotation of last platform in global
        /// </summary>
        float totalOffsetRotation { get; }
        /// <summary>
        /// vector of last platform ending position
        /// </summary>
        Vector3 totalOffset { get; }
        List<PlatformHolder> platformHolders { get; }

        /// <summary>
        /// return platform at distance, if neceassary creates new
        /// </summary>
        IPlatform GetPlatformAt(float distance);

        /// <summary>
        /// return platform at distance and local distance, if neceassary creates new
        /// </summary>
        /// <param name="localDistance">distance at returned platform</param>
        /// <returns></returns>
        IPlatform GetPlatformAt(float distance, out float localDistance);
        void DestroyPlatformsBefore(float distance);
    }

    /// <summary>
    /// holder for list
    /// </summary>
    public sealed class PlatformHolder
    {
        /// <summary>
        /// summ distance of all platforms before
        /// </summary>
        public readonly float generatedLength;
        public readonly IPlatform platform;

        public PlatformHolder(float generatedLength, IPlatform platform)
        {
            this.generatedLength = generatedLength;
            this.platform = platform;
        }
    }
}