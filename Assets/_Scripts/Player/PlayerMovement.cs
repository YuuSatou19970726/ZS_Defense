using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

namespace ZSDefense
{
    public class PlayerMovement : PlayerBase
    {
        private float speed = 6.0f;
        private float jumpSpeed = 8.0f;
        private float gravity = 20f;

        private Vector3 movement;
        private Quaternion rotation = Quaternion.identity;
        private Vector3 moveDirection = Vector3.zero;

        Vector3 offset;
        Vector3 desiredPosition;

        float attackDelay = 1.0f;

        public new Camera camera;

        private void Start()
        {
            this.offset = new Vector3(0, CameraThirdPersonSetup.yOffset, -1f * CameraThirdPersonSetup.distance);

            if (!photonView.IsMine)
            {
                camera.enabled = false;
            }
        }

        private void Update()
        {
            this.CheckAreaAttack();

            if (this.IsDead())
            {
                if (Time.timeSinceLevelLoad - this.timeDie > 9f)
                    this.Respawn();
            }
        }

        private void FixedUpdate()
        {
            if (!this.IsDead())
            {
                this.Attack();
                this.Movement();
            }
            this.MoveToCamera();
        }

        private void Attack()
        {
            if (photonView.IsMine)
            {
                if (Time.time - this.timeAttackStart > this.attackDelay)
                {
                    if (InputManager.Instance.CheckedLeftClick)
                    {
                        this.animator.SetTrigger(AnimationTags.ATTACK_TRIGGER);
                        photonView.RPC("SyncAnimationAttack", RpcTarget.Others);
                        this.timeAttackStart = Time.time;
                    }
                }
            }
        }

        private void Movement()
        {
            if (!photonView.IsMine) return;
            if (this.characterController.isGrounded)
            {
                this.moveDirection = new Vector3(InputManager.Instance.XDirection, 0f, InputManager.Instance.YDirection);
                this.moveDirection *= this.speed;

                if (InputManager.Instance.CheckedJump)
                {
                    this.moveDirection.y = jumpSpeed;
                    this.animator.SetTrigger(AnimationTags.JUMP_TRIGGER);
                    photonView.RPC("SyncAnimationJump", RpcTarget.Others);
                }
            }

            this.movement.Set(InputManager.Instance.XDirection, 0f, InputManager.Instance.YDirection);
            this.movement.Normalize();

            Vector3 desiradForward = Vector3.RotateTowards(transform.forward, movement, 30f + Time.deltaTime, 0f);
            this.rotation = Quaternion.LookRotation(desiradForward);
            transform.rotation = this.rotation;

            this.moveDirection.y -= gravity * Time.deltaTime;



            this.characterController.Move(moveDirection * Time.deltaTime);
            this.animator.SetFloat(AnimationTags.MOVE_FLOAT, movement.magnitude);
            photonView.RPC("SyncAnimationMove", RpcTarget.Others, movement);

            photonView.RPC("SyncPosition", RpcTarget.Others, transform.position);
            photonView.RPC("SyncRotation", RpcTarget.OthersBuffered, transform.rotation);
        }

        private void MoveToCamera()
        {
            if (camera == null) return;
            if (photonView.IsMine)
            {
                this.desiredPosition = transform.position + offset;
                camera.transform.position = Vector3.Lerp(transform.position, desiredPosition, CameraThirdPersonSetup.smoothSpeed * Time.deltaTime);
                camera.transform.LookAt(transform.position);
            }
        }

        [PunRPC]
        void SyncPosition(Vector3 newPosition)
        {
            if (!photonView.IsMine)
            {
                transform.position = newPosition;
            }
        }

        [PunRPC]
        void SyncRotation(Quaternion newRotation)
        {
            if (!photonView.IsMine)
            {
                transform.rotation = newRotation;
            }
        }

        [PunRPC]
        void SyncAnimationMove(Vector3 movement)
        {
            if (!photonView.IsMine)
            {
                this.animator.SetFloat(AnimationTags.MOVE_FLOAT, movement.magnitude);
            }
        }

        [PunRPC]
        void SyncAnimationJump()
        {
            if (!photonView.IsMine)
            {
                this.animator.SetTrigger(AnimationTags.JUMP_TRIGGER);
            }
        }

        [PunRPC]
        void SyncAnimationAttack()
        {
            if (!photonView.IsMine)
            {
                this.animator.SetTrigger(AnimationTags.ATTACK_TRIGGER);
            }
        }
    }
}