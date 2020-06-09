using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class WorldController: IWorldController
    {
        protected readonly IWorldPlatforms worldPlatforms;

        public Pose ElevatePose(float distance, float t)
        {
            Vector3 leftLine, rightLine;
            ElevateLines(distance, out leftLine, out rightLine);

            return new Pose(
                Vector3.Lerp(leftLine, rightLine, t),
                Quaternion.LookRotation(rightLine - leftLine, Vector3.up) * Quaternion.Euler(0f, 180f, 0f));
        }

        public void ElevateLines(float distance, out Vector3 leftLine, out Vector3 rightLine)
        {
            float localDistance;
            Vector3 localLeftLine, localRightLine;
            IPlatform platform = worldPlatforms.GetPlatformAt(distance, out localDistance);
            platform.ElevateLines(localDistance, out localLeftLine, out localRightLine);

            leftLine = // rotate offset before adding
                Quaternion.Euler(0f, platform.totalOffsetRotation, 0f)
                 * localLeftLine
                 + platform.totalOffset;
            rightLine = // rotate offset before adding
                Quaternion.Euler(0f, platform.totalOffsetRotation, 0f)
                 * localRightLine
                 + platform.totalOffset;
        }

        public void LoadWorldUpTo(float from, float to)
        {
            if (worldPlatforms.platformHolders.Count > 0)
            {
                if (worldPlatforms.platformHolders[worldPlatforms.platformHolders.Count - 1].generatedLength < to)
                    worldPlatforms.GetPlatformAt(to);
            }
            else
                worldPlatforms.GetPlatformAt(to);

            foreach (PlatformHolder platformHolder in worldPlatforms.platformHolders)
            {
                if (platformHolder.generatedLength > from && platformHolder.generatedLength < to)
                    if (platformHolder.platform is MonoBehaviour)
                        (platformHolder.platform as MonoBehaviour).gameObject.SetActive(true);
            }
        }
            

        public void HideWorldBefore(float distance)
        {
            if (distance < 0f)
                return;

            foreach (PlatformHolder platformHolder in worldPlatforms.platformHolders)
            {
                if (platformHolder.generatedLength < distance)
                    if (platformHolder.platform is MonoBehaviour)
                        (platformHolder.platform as MonoBehaviour).gameObject.SetActive(false);
            }
        }

        public WorldController(IWorldPlatforms worldPlatforms) => 
            this.worldPlatforms = worldPlatforms;
    }
}