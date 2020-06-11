using System;
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// holds obstacles to spawn on platform
    /// </summary>
    [CreateAssetMenu(menuName = nameof(LinesRunner) + "/" + nameof(ObstaclesHolderSO))]
    public sealed class ObstaclesHolderSO : BaseWeightHolder
    {
        [SerializeField]
        private ObstacleWeightHolder[] _obstacles;
        public ObstacleWeightHolder[] obstacles { get => _obstacles; private set => _obstacles = value; }
        public override WeightHolder[] collection { get => obstacles; protected set => obstacles = value as ObstacleWeightHolder[]; }

        public new ObstacleWeightHolder GetNextByWeigth()
        {
            return base.GetNextByWeigth() as ObstacleWeightHolder;
        }
    }

    /// <summary>
    /// holder for array
    /// </summary>
    [Serializable]
    public sealed class ObstacleWeightHolder : WeightHolder
    {
        [SerializeField]
        private GameObject _obstacle;
        public GameObject obstacle { get => _obstacle; private set => _obstacle = value; }
        public override object obj { get => obstacle; protected set => obstacle = value as GameObject; }
        /// <summary>
        /// count of lines which obstacle takes for
        /// </summary>
        [SerializeField, Range(1, GameConst.linesCount)]
        public int _linesCount;
        public int linesCount { get => _linesCount; private set => _linesCount = value; }
    }
}