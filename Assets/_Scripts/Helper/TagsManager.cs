using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSDefense
{
    public class SceneTags
    {
        public const string MAIN_MENU = "MainMenu";
        public const string AREA_0 = "Area_00";
    }

    public class Tags
    {
        public const string PLAYER = "Player";
    }

    public class GameObjectTags
    {
        public const string AREA_ATTACK = "AreaAttack";
        public const string ENEMY_AREA_ATTACK = "EnemyAreaAttack";
    }

    public class AxisTags
    {
        public const string HORIZONTAL = "Horizontal";
        public const string VERTICAL = "Vertical";
    }

    public class GetButtonTags
    {
        public const string BUTTON_JUMP = "Jump";
        public const string BUTTON_FIRE_1 = "Fire1";
    }

    public class AnimationTags
    {
        public const string IDLE_KNIGHT = "Idle";
        public const string DIE_KNIGHT = "Die";
        public const string ATTACK_TRIGGER = "Attack";
        public const string JUMP_TRIGGER = "Jump";
        public const string MOVE_FLOAT = "Move";
    }

    public class PlayerPrefTags
    {
        public const string PLAYER_NAME = "playerName";
    }
}