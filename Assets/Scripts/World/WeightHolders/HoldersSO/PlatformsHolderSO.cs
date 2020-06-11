using System;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// holds platforms to spawn in world
    /// </summary>
    [CreateAssetMenu(menuName = nameof(LinesRunner) + "/" + nameof(PlatformsHolderSO))]
    public sealed class PlatformsHolderSO : BaseWeightHolder
    {
        [SerializeField]
        private PlatformWeightHolder[] _platforms;
        public PlatformWeightHolder[] platforms { get => _platforms; private set => _platforms = value; }
        public override WeightHolder[] collection { get => platforms; protected set => platforms = value as PlatformWeightHolder[]; }

        public new PlatformWeightHolder GetNextByWeigth()
        {
            return base.GetNextByWeigth() as PlatformWeightHolder;
        }
    }

    /// <summary>
    /// holder for array
    /// </summary>
    [Serializable]
    public sealed class PlatformWeightHolder : WeightHolder
    {
        [SerializeField]
        private PlatformBase _platform;
        public PlatformBase platform { get => _platform; private set => _platform = value; }
        public override object obj { get => platform; protected set => platform = value as PlatformBase; }
    }
}