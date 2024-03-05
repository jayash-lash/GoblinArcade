using System;
using Enemy.Enum;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "EnemiesData", menuName = "Data/EnemiesData")]
    [Serializable]
    public class EnemiesData : ScriptableObject
    {
        public EnemyConfig[] CharactersPrefab;

        public EnemyConfig GetData(EnemyType type)
        {
            foreach (var character in CharactersPrefab)
            {
                if (character.EnemyPrefab.EnemyType == type)
                    return character;
            }

            return null;
        }
    }
}