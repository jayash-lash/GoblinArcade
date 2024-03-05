using System;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Data/PlayerData")]
    [Serializable]
    public class PlayerConfig : BaseDataConfig
    {
        public float Damage = 2f;
        public float StrongDamage = 10f;
        public float AttackRange = 2f;
        public float TimeBetweenAttacks = 1f;
        public float TimeBetweenStrongAttacks = 2f;
    }
}