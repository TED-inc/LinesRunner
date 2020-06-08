using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class WorldController: IWorldController
    {
        protected readonly IWorldPlatforms worldPlatforms;

        public void ElevateLines(float distance, out Vector3 leftLine, out Vector3 rightLine)
        {
            float localDistance;
            Vector3 localLeftLine, localRightLine;
            worldPlatforms.GetPlatformAt(distance, out localDistance).ElevateLines(localDistance, out localLeftLine, out localRightLine);
            leftLine = // rotate offset before adding
                Quaternion.Euler(0f, worldPlatforms.GetTotalOffsetRotationAt(distance), 0f)
                 * localLeftLine
                 + worldPlatforms.GetTotalOffsetAt(distance);
            rightLine = // rotate offset before adding
                Quaternion.Euler(0f, worldPlatforms.GetTotalOffsetRotationAt(distance), 0f)
                 * localRightLine
                 + worldPlatforms.GetTotalOffsetAt(distance);
        }

        public void LaodWorldUpTo(float distance) => 
            worldPlatforms.GetPlatformAt(distance);



        public WorldController(IWorldPlatforms worldPlatforms) => 
            this.worldPlatforms = worldPlatforms;
    }
}