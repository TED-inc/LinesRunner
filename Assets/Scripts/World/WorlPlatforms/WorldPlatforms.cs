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
        public List<PlatformHolder> platformHolders { get; protected set; } = new List<PlatformHolder>();

        protected readonly IPlatformsFactory platformsFactory;
        protected readonly IObstaclesFactory obstaclesFactory;



        public IPlatform GetPlatformAt(float distance)
        {
            float localDistance;
            return GetPlatformAt(distance, out localDistance);
        }

        public IPlatform GetPlatformAt(float distance, out float localDistance)
        {
            localDistance = distance;

            if (CheckDistance())
                return null;

            return FindPlatform(ref localDistance);



            bool CheckDistance()
            {
                if (distance < 0f)
                {
                    Debug.LogError("[WLP] GetPlatform : negetive distance");
                    return true;
                }

                while (distance >= generatedPlatformsLength)
                    AddNewPlatform();

                return false;
            }

            IPlatform FindPlatform(ref float localDist)
            {
                //check for low distnce
                if (platformHolders[0].generatedLength >= distance)
                    return platformHolders[0].platform;

                //find platform for distnce
                for (int i = 0; i < platformHolders.Count - 1; i++)
                    if (platformHolders[i].generatedLength < distance && platformHolders[i + 1].generatedLength >= distance)
                    {
                        localDist = distance - platformHolders[i].generatedLength;
                        return platformHolders[i + 1].platform;
                    }

                Debug.LogError("[WLP] GetPlatform : logic error");
                return platformHolders[platformHolders.Count - 1].platform;
            }
        }

        protected void AddNewPlatform()
        {
            IPlatform platform = platformsFactory.GetNextPlatform(totalOffset, totalOffsetRotation);
            platformHolders.Add(
                new PlatformHolder(
                    generatedPlatformsLength + platform.length,
                    platform));

            platform.SetWorldData(totalOffsetRotation, totalOffset);

            totalOffset += Quaternion.Euler(0f, totalOffsetRotation, 0f) * platform.offset; //rotate offset before adding
            totalOffsetRotation += platform.offsetRotation;
            totalOffsetRotation %= 360f; //clamp rotation

            if (platform is PlatformBase)
                obstaclesFactory.SetObstacles(platform as PlatformBase);
        }

        public void DestroyPlatformsBefore(float distance)
        {
            if (distance < 0f)
                return;
                
            while (platformHolders[0].generatedLength < distance)
            {
                if (platformHolders[0].platform is MonoBehaviour)
                    GameObject.Destroy((platformHolders[0].platform as MonoBehaviour).gameObject);
            
                platformHolders.RemoveAt(0);
            }
        }


        public WorldPlatforms(IPlatformsFactory platformsFactory, IObstaclesFactory obstaclesFactory)
        {
            this.platformsFactory = platformsFactory;
            this.obstaclesFactory = obstaclesFactory;
        }
    }
}