using System;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    [CreateAssetMenu(menuName = nameof(LinesRunner) + "/" + nameof(PlatformsHolderSO))]
    public sealed class PlatformsHolderSO : ScriptableObject
    {
        public PlatformWithWeight[] platforms;

        [Serializable]
        public class PlatformWithWeight
        {
            public PlatformBase platform;
            public float weight = 1f;
        }
    }
}