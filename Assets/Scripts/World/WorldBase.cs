using System.Collections.Generic;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public abstract class WorldBase : IWorld
    {
        protected List<PlatformHolder> platformHolders = new List<PlatformHolder>();

        public float generatedPlatformsLength => 
            platformHolders.Count == 0 ?
                0f : 
                platformHolders[platformHolders.Count - 1].generatedLength;

        protected readonly IPlatformsFactory platformsFactory;

        public virtual IPlatform GetPlatformAt(float distance)
        {
            //add new platforms if it necessary
            while (distance >= generatedPlatformsLength)
                AddNewPlatform();

            //check for low distnce
            if (platformHolders[0].generatedLength >= distance)
                return platformHolders[0].platform;

            //find platform for distnce
            for (int i = 0; i < platformHolders.Count - 1; i++)
                if (platformHolders[i].generatedLength < distance && platformHolders[i + 1].generatedLength >= distance)
                    return platformHolders[i + 1].platform;

            Debug.LogError("[WRD] GetPlatform logic error");
            return platformHolders[platformHolders.Count - 1].platform;


            void AddNewPlatform()
            {
                IPlatform platform = platformsFactory.GetNextPlatform();
                platformHolders.Add(
                    new PlatformHolder(
                        generatedPlatformsLength + platform.length,
                        platform));
            }
        }

        public WorldBase(IPlatformsFactory platformsFactory)
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