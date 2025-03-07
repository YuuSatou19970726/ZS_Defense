using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

namespace ZSDefense
{
    public class PlayerUI : MonoBehaviourPun
    {
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 120f, 0f);

        [SerializeField]
        private Text playerNameText;

        [SerializeField]
        private Slider playerHealthSlider;

        float characterControllerHeight;

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        public void SetInfoPlayer(PlayerManager playerManager)
        {
            CharacterController characterController = playerManager.GetComponent<CharacterController>();

            if (characterController != null)
                this.characterControllerHeight = characterController.height;


            this.playerNameText.text = "Player Name";

            if (playerNameText != null)
                this.playerNameText.text = playerManager.photonView.Owner.NickName;
        }

        public void SetHealthPlayer(int maxHealth, int health)
        {
            if (this.playerHealthSlider != null)
            {
                playerHealthSlider.maxValue = maxHealth;
                playerHealthSlider.value = health;
            }
        }

        public void SetPositionPlayer(Transform targetTransform)
        {
            Vector3 targetPosition;
            targetPosition = targetTransform.position;
            targetPosition.y = this.characterControllerHeight;

            this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
        }
    }
}