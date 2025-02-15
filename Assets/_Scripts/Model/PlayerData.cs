using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSDefense
{
    public class PlayerData
    {
        // public int playerId;
        public string playerName;
        public int health;
        public int maxHealth;
        public int mana;
        public int maxMana;

        public float initialX;
        public float initialY;

        public PlayerData(string playerName, int health, int maxHealth, int mana, int maxMana, float initialX, float initialY)
        {
            this.playerName = playerName;
            this.health = health;
            this.maxHealth = maxHealth;
            this.mana = mana;
            this.maxMana = maxMana;
            this.initialX = initialX;
            this.initialY = initialY;
        }
    }
}