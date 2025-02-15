using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSDefense
{
    [RequireComponent(typeof(PlayerMovement))]
    [RequireComponent(typeof(PlayerTask))]

    public class PlayerController : CustomMonobehaviour
    {
        public PlayerMovement playerMovement;
        public PlayerTask playerTask;

        protected override void LoadComponents()
        {
            this.PlayerMovement();
            this.PlayerTask();
        }

        private void PlayerMovement()
        {
            if (this.playerMovement != null) return;
            this.playerMovement = GetComponent<PlayerMovement>();
        }

        private void PlayerTask()
        {
            if (this.playerTask != null)
                this.playerTask = GetComponent<PlayerTask>();
        }
    }
}