using System.Collections.Generic;

namespace TEDinc.LinesRunner
{
    public interface IWorld
    {
        float generatedPlatformsLength { get; }
        IPlatform GetPlatformAt(float distance);
    }
}