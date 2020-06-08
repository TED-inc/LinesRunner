using UnityEngine;

namespace TEDinc.LinesRunner
{
    public static class WorldPlatformsUtility
    {
        public static float GetTotalOffsetRotationAt(this IWorldPlatforms worldPlatforms, float distance)
        {
            //just for fill platforms if it empty
            worldPlatforms.GetPlatformAt(distance);

            float offsetRotation = 0f;
            if (worldPlatforms.platformHolders[0].generatedLength < distance)
                offsetRotation += worldPlatforms.platformHolders[0].platform.offsetRotation;

            for (int i = 0; i < worldPlatforms.platformHolders.Count - 1; i++)
            {
                if (worldPlatforms.platformHolders[i + 1].generatedLength >= distance)
                    break;
                else
                    offsetRotation += worldPlatforms.platformHolders[i + 1].platform.offsetRotation;
            }
            offsetRotation %= 360f; //clamp rotation

            return offsetRotation;
        }

        public static Vector3 GetTotalOffsetAt(this IWorldPlatforms worldPlatforms, float distance)
        {
            //just for fill platforms if it empty
            worldPlatforms.GetPlatformAt(distance);

            Vector3 offset = Vector3.zero;

            if (worldPlatforms.platformHolders[0].generatedLength < distance)
                offset += worldPlatforms.platformHolders[0].platform.offset;

            for (int i = 1; i < worldPlatforms.platformHolders.Count; i++)
            {
                if (worldPlatforms.platformHolders[i].generatedLength >= distance)
                    break;
                else
                    offset += // rotate offset before adding
                            Quaternion.Euler(0f, worldPlatforms.GetTotalOffsetRotationAt(worldPlatforms.platformHolders[i].generatedLength), 0f)
                            * worldPlatforms.platformHolders[i].platform.offset;
            }

            return offset;
        }
    }
}