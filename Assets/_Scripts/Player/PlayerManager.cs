using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ZSDefense
{
    public class PlayerManager : MonoBehaviourPunCallbacks, IPunObservable
    {
        private PlayerController playerController;

        private static GameObject localPlayerInstance;
        public static GameObject LocalPlayerInstance => localPlayerInstance;

        // private bool leavingRoom = false;

        private void Awake()
        {
            this.playerController = gameObject.GetComponent<PlayerController>();

            if (photonView.IsMine)
                localPlayerInstance = gameObject;

            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            if (photonView.IsMine)
                UIManager.Instance.CreatePlayerUIPrefabs(this);

            InputManager.Instance.SetProcessInputs(true);
        }

        private void Update()
        {
            if (!photonView.IsMine) return;

            // if (playerController.playerMovement.IsDead() && !this.leavingRoom)
            // {
            //     this.leavingRoom = PhotonNetwork.LeaveRoom();
            //     InputManager.Instance.SetProcessInputs(false);
            // }
            UIManager.Instance.UpdateInfoPlayer(this);
        }

        private void FixedUpdate()
        {
            if (!photonView.IsMine) return;
            UIManager.Instance.UpdatePositionPlayer(this, transform);
        }

        public override void OnDisable()
        {
            base.OnDisable();
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
            if (!photonView.IsMine) return;

            if (other.gameObject.name == GameObjectTags.ENEMY_AREA_ATTACK)
            {
                playerController.playerMovement.TakeDamage(1);
            }
        }

        public override void OnLeftRoom()
        {
            // this.leavingRoom = false;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (this.playerController == null) return;

            if (stream.IsWriting)
            {
                // We own this player: send the others our data
                stream.SendNext(this.playerController.playerMovement.Health);
            }
            else
            {
                // Network player, receive data
                this.playerController.playerMovement.SetHealth((int)stream.ReceiveNext());
            }
        }
    }
}