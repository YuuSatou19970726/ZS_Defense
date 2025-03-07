using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ZSDefense
{
    public class PlayerBase : MonoBehaviourPun, IPunObservable
    {
        protected PlayerData playerData;
        public int Health => playerData.health;
        public int MaxHealth => playerData.maxHealth;

        protected CharacterController characterController;
        protected Animator animator;

        private GameObject areaAttack;
        protected float timeAttackStart = -1f;
        protected float timeDie = -1f;

        private void Awake()
        {
            this.LoadPlayerData();
            this.LoadComponents();
        }

        protected void LoadComponents()
        {
            this.characterController = GetComponent<CharacterController>();
            this.animator = GetComponent<Animator>();
            this.areaAttack = transform.Find(GameObjectTags.AREA_ATTACK).gameObject;
        }

        private void LoadPlayerData()
        {
            playerData = new PlayerData("NULL", 10, 10, 10, 10, transform.position.x, transform.position.y);
        }

        private bool IsAttack()
        {
            return Time.timeSinceLevelLoad - this.timeAttackStart < 0 / 2f;
        }

        protected void CheckAreaAttack()
        {
            this.areaAttack.SetActive(IsAttack());
        }

        public void SetHealth(int health)
        {
            this.playerData.health = health;
        }

        public void TakeDamage(int takeDamage)
        {
            this.playerData.health -= takeDamage;
        }

        public bool IsDead()
        {
            return this.playerData.health <= 0;
        }

        protected virtual void Die()
        {
            if (this.timeDie < 0)
            {
                this.timeDie = Time.timeSinceLevelLoad;
                this.animator.SetBool(AnimationTags.DIE_KNIGHT, true);
            }

            if (Time.timeSinceLevelLoad - this.timeDie > 2f)
                transform.localScale = Vector3.zero;
        }

        protected virtual void Respawn()
        {
            transform.position = new Vector3(this.playerData.initialX, 0, this.playerData.initialY);
            this.animator.SetBool(AnimationTags.DIE_KNIGHT, false);
            transform.localScale = Vector3.one;
            this.timeDie = -1;
            this.playerData.health = this.playerData.maxHealth;
        }

        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(transform.rotation);
            }
            else
            {
                transform.position = (Vector3)stream.ReceiveNext();
                transform.rotation = (Quaternion)stream.ReceiveNext();
            }
        }
    }
}