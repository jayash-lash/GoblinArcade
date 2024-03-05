using Enemy;
using Enemy.Enum;
using Factories;
using ScriptableObjects;
using UnityEngine;
using Zenject;

namespace Services
{
    public class ObjectSpawner
    {
        private LevelConfig _levelConfig;
        private SimpleEnemyFactory _simpleFactory;
        private DividingEnemyFactory _dividingFactory;
        private MiniEnemyFactory _miniEnemyFactory;

        [Inject]
        private void Construct(SimpleEnemyFactory simpleFactory, LevelConfig levelConfig, 
            DividingEnemyFactory dividingFactory, MiniEnemyFactory miniEnemyFactory)
        {
            _levelConfig = levelConfig;
            _simpleFactory = simpleFactory;
            _dividingFactory = dividingFactory;
            _miniEnemyFactory = miniEnemyFactory;
        }

        private float _simpleEnemyScale = 0.015f;
        private float _dividedEnemyScale = 0.0075f;


        public void SpawnObject(int index)
        {
            var waveConfig = _levelConfig.Waves[index];
            var totalCharacters = waveConfig.Characters.Length;

            float chance = 0f;
            if (totalCharacters > 1)
            {
                var spawnChance = Random.Range(0f, 1f);

                for (var i = 0; i < totalCharacters; i++)
                {
                    var characterChance = (float)(i + 1) / totalCharacters;
                    chance += characterChance;

                    if (!(spawnChance <= chance)) continue;
                    
                    if (waveConfig.Characters[i] == EnemyType.SimpleEnemy)
                        SpawnSimpleEnemy();
                    else if (waveConfig.Characters[i] == EnemyType.DivideEnemy) SpawnDividedEnemy();

                    break;
                }
            }
            else
            {
                foreach (var enemyType in waveConfig.Characters)
                {
                    if (enemyType == EnemyType.SimpleEnemy)
                        SpawnSimpleEnemy();
                    else if (enemyType == EnemyType.DivideEnemy) 
                        SpawnDividedEnemy();
                }
            }
        }


        private void SpawnSimpleEnemy()
        {
            _simpleFactory.CreateObject(CalculateRandomSpawnPoint(), _simpleEnemyScale);
        }

        private void SpawnDividedEnemy()
        {
            var dividedEnemy = _dividingFactory.CreateObject(CalculateRandomSpawnPoint(), _simpleEnemyScale);

            dividedEnemy.EnemyHealth.OnHealthZero += SpawnDividedObject;

            void SpawnDividedObject()
            {
                SpawnDivideObject(dividedEnemy);
                dividedEnemy.EnemyHealth.OnHealthZero -= SpawnDividedObject;
            }
        }

        private void SpawnDivideObject(BaseEnemy enemy)
        {
            var position = enemy.transform.position;
            
            _miniEnemyFactory.CreateObject(position  + new Vector3(-1f, 0f, -1f), _dividedEnemyScale);
            _miniEnemyFactory.CreateObject(position + new Vector3(1f, 0f, 1f), _dividedEnemyScale);
        }


        private static Vector3 CalculateRandomSpawnPoint()
        {
            var pos = new Vector3(Random.Range(-10, 10), 0, Random.Range(-10, 10));

            return pos;
        }
    }
}