using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class WorldPlatforms : IWorldPlatforms
    {
        public float generatedPlatformsLength =>
            platformHolders.Count == 0 ?
                0f :
                platformHolders[platformHolders.Count - 1].generatedLength;
        public float totalOffsetRotation { get; protected set; } = 0f;
        public Vector3 totalOffset { get; protected set; } = Vector3.zero;

        protected List<PlatformHolder> platformHolders = new List<PlatformHolder>();
        protected readonly IPlatformsFactory platformsFactory;



        public IPlatform GetPlatformAt(float distance)
        {
            float localDistance;
            return GetPlatformAt(distance, out localDistance);
        }

        public IPlatform GetPlatformAt(float distance, out float localDistance)
        {
            localDistance = distance;
            if (distance < 0f)
            {
                Debug.LogError("[WLD] GetPlatform : negetive distance");
                return null;
            }

            //add new platforms if it necessary
            while (distance >= generatedPlatformsLength)
                AddNewPlatform();

            //check for low distnce
            if (platformHolders[0].generatedLength >= distance)
                return platformHolders[0].platform;

            //find platform for distnce
            for (int i = 0; i < platformHolders.Count - 1; i++)
                if (platformHolders[i].generatedLength < distance && platformHolders[i + 1].generatedLength >= distance)
                {
                    localDistance = distance - platformHolders[i].generatedLength;
                    return platformHolders[i + 1].platform;
                }

            Debug.LogError("[WLD] GetPlatform : logic error");
            return platformHolders[platformHolders.Count - 1].platform;
        }

        protected void AddNewPlatform()
        {
            IPlatform platform = platformsFactory.GetNextPlatform(totalOffset, totalOffsetRotation);
            platformHolders.Add(
                new PlatformHolder(
                    generatedPlatformsLength + platform.length,
                    platform));

            totalOffset += Quaternion.Euler(0f, totalOffsetRotation, 0f) * platform.offset; //rotate offset before adding
            totalOffsetRotation += platform.offsetRotation;
            totalOffsetRotation %= 360f; //clamp rotation
        }

        public float GetTotalOffsetRotationAt(float distance)
        {
            //just for fill platforms if it empty
            GetPlatformAt(distance);

            float offsetRotation = 0f;
            if (platformHolders[0].generatedLength < distance)
                offsetRotation += platformHolders[0].platform.offsetRotation;

            for (int i = 0; i < platformHolders.Count - 1; i++)
            {
                if (platformHolders[i + 1].generatedLength >= distance)
                    break;
                else
                    offsetRotation += platformHolders[i + 1].platform.offsetRotation;
            }
            offsetRotation %= 360f; //clamp rotation

            return offsetRotation;
        }

        public Vector3 GetTotalOffsetAt(float distance)
        {
            //just for fill platforms if it empty
            GetPlatformAt(distance);

            Vector3 offset = Vector3.zero;

            if (platformHolders[0].generatedLength < distance)
                offset += platformHolders[0].platform.offset;

            for (int i = 0; i < platformHolders.Count - 1; i++)
            {
                if (platformHolders[i + 1].generatedLength >= distance)
                    break;
                else
                    offset += // rotate offset before adding
                        Quaternion.Euler(0f, platformHolders[i].platform.offsetRotation, 0f)
                        * platformHolders[i + 1].platform.offset;
            }

            return offset;
        }



        public WorldPlatforms(IPlatformsFactory platformsFactory)
        {
            this.platformsFactory = platformsFactory;
        }



        protected sealed class PlatformHolder
        {
            public readonly float generatedLength;
            public readonly IPlatform platform;

            public PlatformHolder(float generatedLength, IPlatform platform)
            {
                this.generatedLength = generatedLength;
                this.platform = platform;
            }
        }
    }
}