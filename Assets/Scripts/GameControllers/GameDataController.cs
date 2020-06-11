#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// controls and displays data for player
    /// </summary>
    public sealed class GameDataController : MonoBehaviour
    {
        public static GameDataController instance;
        [SerializeField]
        private TMP_Text coinsByRun;
        [SerializeField]
        private TMP_Text oldMaxDistance;

        private List<Del> onCoinCollected { get; } = new List<Del>();
        private List<Del> onDistanceChanged { get; } = new List<Del>();
        private List<Del> onMaxDistanceChanged { get; } = new List<Del>();

        private int coinsCountOnStart;
        private int oldMaxDistanceOnStart;
       

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Debug.LogError("[GRC] Only one instance must be in game!");
        }



        /// <summary>
        /// return list of delegates to subscribe/unsubscribe of it
        /// </summary>
        public List<Del> GetDelegatesByPlayerPrefs(GameConst.PlayerPrefs playerPrefs)
        {
            switch (playerPrefs)
            {
                case GameConst.PlayerPrefs.coin:
                    return onCoinCollected;
                case GameConst.PlayerPrefs.distance:
                    return onDistanceChanged;
                case GameConst.PlayerPrefs.maxDistance:
                    return onMaxDistanceChanged;
                default:
                    return null;
            }
        }

        /// <summary>
        /// invoke all delegates subscribed to parametr
        /// </summary>
        public void InvokeByPlayerPrefs(GameConst.PlayerPrefs playerPrefs)
        {
            foreach (Del deleg in GetDelegatesByPlayerPrefs(playerPrefs))
                deleg.Invoke();
        }

        /// <summary>
        /// refresh visible on start
        /// </summary>
        public void RefreshStartData()
        {
            if (PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.maxDistance)) < PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.distance)))
                PlayerPrefs.SetInt(nameof(GameConst.PlayerPrefs.maxDistance), PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.distance)));

            coinsCountOnStart = PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.coin));
            oldMaxDistanceOnStart = PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.maxDistance));
        }

        /// <summary>
        /// refresh visible on end
        /// </summary>
        public void RefreshEndData()
        {
            coinsByRun.text = (PlayerPrefs.GetInt(nameof(GameConst.PlayerPrefs.coin)) - coinsCountOnStart).ToString();
            oldMaxDistance.text = oldMaxDistanceOnStart.ToString();
        }
    }
}