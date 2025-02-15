using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSDefense;

namespace ZSDefense
{
    public class PlayerTask : CustomMonobehaviour
    {
        private PlayerController playerController;

        protected override void LoadComponents()
        {
            this.playerController = GetComponent<PlayerController>();
        }

        void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.name == GameObjectTags.ENEMY_AREA_ATTACK)
            {
                playerController.playerMovement.TakeDamage(1);
            }
        }
    }
}