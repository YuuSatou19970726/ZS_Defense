using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;

namespace ZSDefense
{
    public class UIManager : MonoBehaviour
    {
        private static UIManager instance;
        public static UIManager Instance => instance;

        [SerializeField]
        private GameObject playerUiPrefab;

        private Dictionary<PlayerManager, PlayerUI> playerDictionary = new Dictionary<PlayerManager, PlayerUI>();

        private void Awake()
        {
            if (instance == null)
                instance = this;
        }

        public void UpdateInfoPlayer(PlayerManager playerManager)
        {
            if (!this.playerDictionary.ContainsKey(playerManager)) return;
            int maxHealth = playerManager.GetMaxHealth();
            int health = playerManager.GetHealth();
            this.playerDictionary[playerManager].SetHealthPlayer(maxHealth, health);
        }

        public void UpdatePositionPlayer(PlayerManager playerManager, Transform playerTransform)
        {
            if (!this.playerDictionary.ContainsKey(playerManager)) return;
            this.playerDictionary[playerManager].SetPositionPlayer(playerTransform);
        }

        public void CreatePlayerUIPrefabs(PlayerManager playerManager)
        {
            GameObject uiPlayer = Instantiate(this.playerUiPrefab);
            PlayerUI playerUI = uiPlayer.GetComponent<PlayerUI>();
            playerUI.SendMessage("SetInfoPlayer", playerManager, SendMessageOptions.RequireReceiver);
            this.playerDictionary.Add(playerManager, playerUI);
        }
    }
}