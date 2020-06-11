using UnityEngine;

namespace TEDinc.LinesRunner
{
    public class CoinCollectable : CollectableBase
    {
        public override string playerPrefsName { get; } = nameof(GameConst.PlayerPrefs.coin);

        public override void Collect()
        {
            if (!collected)
            {
                PlayerPrefs.SetInt(nameof(GameConst.PlayerPrefs.coin), PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.coin)) + 1);
                GameDataController.instance.InvokeByPlayerPrefs(GameConst.PlayerPrefs.coin);
            }
            collected = true;
            base.Collect();
        }
    }
}