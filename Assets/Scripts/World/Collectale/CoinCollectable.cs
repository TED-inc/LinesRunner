using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class CoinCollectable : CollectableBase
    {
        public override string playerPrefsName { get; } = GameConst.playerPrefsCoin;
    }
}