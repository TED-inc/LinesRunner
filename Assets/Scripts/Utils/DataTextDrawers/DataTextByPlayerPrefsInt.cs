#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;
using TMPro;

namespace TEDinc.LinesRunner
{
    public sealed class DataTextByPlayerPrefsInt : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text text;
        [SerializeField]
        private GameConst.PlayerPrefs playerPrefs;

        private void Start()
        {
            GameDataController.instance.GetDelegatesByPlayerPrefs(playerPrefs).Add(TextRedraw);
            TextRedraw();

            void TextRedraw() =>
                text.text = PlayerPrefs.GetInt(playerPrefs.ToString()).ToString();
        }

    }
}