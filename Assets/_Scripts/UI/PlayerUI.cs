using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ZSDefense
{
    public class PlayerUI : MonoBehaviour
    {
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

        [SerializeField]
        private Text playerNameText;

        [SerializeField]
        private Slider playerHealthSlider;

        private PlayerManager target;

        float characterControllerHeight;

        Transform targetTransform;

        Vector3 targetPosition;

        private void Awake()
        {
            this.transform.SetParent(GameObject.Find("Canvas").GetComponent<Transform>(), false);
        }

        private void Update()
        {
            if (this.target == null)
            {
                Destroy(this.gameObject);
                return;
            }

            if (this.playerHealthSlider != null)
            {
                playerHealthSlider.maxValue = target.GetMaxHealth();
                playerHealthSlider.value = target.GetHealth();
            }
        }

        private void LateUpdate()
        {
            if (this.targetTransform != null)
            {
                this.targetPosition = this.targetTransform.position;
                this.targetPosition.y = this.characterControllerHeight;

                this.transform.position = Camera.main.WorldToScreenPoint(targetPosition) + screenOffset;
            }
        }

        public void SetTarget(PlayerManager playerManager)
        {
            if (playerManager == null) return;

            this.target = playerManager;
            this.targetTransform = this.target.GetComponent<Transform>();

            CharacterController characterController = this.target.GetComponent<CharacterController>();

            if (characterController != null)
                this.characterControllerHeight = characterController.height;


            this.playerNameText.text = "Player Name";

            if (playerNameText != null)
                this.playerNameText.text = this.target.photonView.Owner.NickName;
        }
    }
}