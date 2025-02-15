using System.Collections;
using System.Collections.Generic;
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

        float attackDelay = 1.0f;

        protected override void Update()
        {
            this.CheckAreaAttack();

            if (this.IsDead())
            {
                if (Time.timeSinceLevelLoad - this.timeDie > 9f)
                    this.Respawn();
            }
        }

        protected override void FixedUpdate()
        {
            if (!this.IsDead())
            {
                this.Attack();
                this.Movement();
            }
        }

        private void Attack()
        {
            if (Time.time - this.timeAttackStart > this.attackDelay)
            {
                if (InputManager.Instance.CheckedLeftClick)
                {
                    this.animator.SetTrigger(AnimationTags.ATTACK_TRIGGER);
                    this.timeAttackStart = Time.time;
                }
            }
        }

        private void Movement()
        {
            if (this.characterController.isGrounded)
            {
                this.moveDirection = new Vector3(InputManager.Instance.XDirection, 0f, InputManager.Instance.YDirection);
                this.moveDirection *= this.speed;

                if (InputManager.Instance.CheckedJump)
                {
                    this.moveDirection.y = jumpSpeed;
                    this.animator.SetTrigger(AnimationTags.JUMP_TRIGGER);
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
        }
    }
}