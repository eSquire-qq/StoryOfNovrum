using UnityEngine;


namespace GlobalConstants{
    public class Tags : ScriptableObject
    {
        public static readonly string ENEMY = "Enemy";
        public static readonly string PLAYER = "Player";

        public static readonly string MASKTRIGGER = "MaskTrigger";
    }

    public class Layers : ScriptableObject
    {
        public static readonly string DEFAULT = "Default";
        public static readonly string MIDDLELAYER = "MiddleLayer";
    }

    public class Animations : ScriptableObject
    {
        public static readonly string ATTACK = "Attack";
        public static readonly string TAKEDAMAGE = "TakeDamage";
        public static readonly string ONEDESTRUCTION = "OnDestruction";
        public static readonly string IDLE = "Idle";
        public static readonly string WALK = "Walk";
    }
    
    public class Parameters : ScriptableObject
    {
        public static readonly string COOLDOWN = "Cooldown";
        public static readonly string DAMAGE = "Damage";
        public static readonly string KNOCKBACK = "KnockBack";
        public static readonly string DURABILITY = "Durability";
    }

    public class Actions : ScriptableObject
    {
        public static readonly string DROP = "Drop";
        public static readonly string MOVE = "Move";
        public static readonly string INTERACT = "Interact";
    }

}