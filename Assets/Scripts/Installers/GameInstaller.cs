using Factories;
using Player;
using ScriptableObjects;
using Services;
using Services.Pool;
using Services.Registry;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [Header("WaveManager")] 
        [SerializeField] private WaveManager _waveManager;
        [Header("ScriptableObject")] 
        [SerializeField] private LevelConfig _levelConfig;
        [SerializeField] private EnemiesData _enemiesData;
        [SerializeField] private PlayerConfig _playerData;
        [Header("Player")]
        [SerializeField] private PlayerObserver _playerObserver;
        [SerializeField] private ClosestEnemyFinder _enemyFinder;
        [Header("Factories")] 
        [SerializeField] private SimpleEnemyFactory _enemyFactory;
        [SerializeField] private DividingEnemyFactory _dividingEnemyFactory;
        [SerializeField] private MiniEnemyFactory _miniEnemyFactory;

        public override void InstallBindings()
        {
            PoolInstaller.Install(Container);
            
            Container.Bind<EnemyRegistry>().AsCached();
            Container.Bind<ObjectSpawner>().AsCached();
            Container.BindInstance(_levelConfig).AsSingle();
            Container.BindInstance(_enemiesData).AsSingle();
            Container.BindInstance(_playerData).AsSingle();
            Container.BindInstance(_waveManager).AsSingle();
            
            Container.Bind<PlayerObserver>().FromInstance(_playerObserver).AsSingle().NonLazy();
            Container.Bind<ClosestEnemyFinder>().FromInstance(_enemyFinder).AsSingle().NonLazy();

            Container.Bind<SimpleEnemyFactory>().FromInstance(_enemyFactory).AsSingle().NonLazy();
            Container.Bind<DividingEnemyFactory>().FromInstance(_dividingEnemyFactory).AsSingle().NonLazy();
            Container.Bind<MiniEnemyFactory>().FromInstance(_miniEnemyFactory).AsSingle().NonLazy();
        }
    }
}