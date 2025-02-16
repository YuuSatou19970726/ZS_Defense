using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ZSDefense
{
    public class PlayerManager : MonoBehaviour
    {
        private PlayerController playerController;

        [SerializeField]
        private GameObject playerUiPrefab;

        private void Awake()
        {
            this.playerController = gameObject.GetComponent<PlayerController>();
        }

        private void Start()
        {
            if (this.playerUiPrefab != null)
            {
                GameObject uiPlayer = Instantiate(this.playerUiPrefab);
                uiPlayer.SendMessage("SetTarget", this, SendMessageOptions.RequireReceiver);
            }
        }

        public int GetHealth()
        {
            return this.playerController.playerMovement.Health;
        }

        public int GetMaxHealth()
        {
            return this.playerController.playerMovement.MaxHealth;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == GameObjectTags.ENEMY_AREA_ATTACK)
            {
                playerController.playerMovement.TakeDamage(1);
            }
        }
    }
}