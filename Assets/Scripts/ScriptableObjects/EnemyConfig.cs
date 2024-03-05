using System;
using Enemy;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "Data/EnemyConfig")]
    [Serializable]

    public class EnemyConfig : BaseDataConfig
    {
        public BaseEnemy EnemyPrefab;
        public float Damage;
    }
}