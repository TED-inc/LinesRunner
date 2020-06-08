using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IPlatformsFactory
    {
        IPlatform GetNextPlatform(Vector3 worldPosition, float worldRotataion);
    }
}