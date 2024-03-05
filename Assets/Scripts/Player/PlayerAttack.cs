using System;
using Cysharp.Threading.Tasks;
using Enemy;
using ScriptableObjects;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        public event Action<bool> OnRangeAttackChanged;
        public event Action OnLightAttack;
        public event Action OnStrongAttack;

        public float StrongAttackDelay { get; private set; }
        public bool IsAttacking { get; private set; }
        
        private PlayerConfig _config;
        private ClosestEnemyFinder _enemyFinder;
        
        [Inject]
        private void Construct(PlayerConfig config, ClosestEnemyFinder enemyFinder)
        {
            _config = config;
            _enemyFinder = enemyFinder;
        }

        private float _simpleAttackDelay;
        private float _lastStrongAttackTime = 0;
        private float _lastLightAttackTime = 0;

        private void Start()
        {
            _simpleAttackDelay = _config.TimeBetweenAttacks;
            StrongAttackDelay = _config.TimeBetweenStrongAttacks;
        }

        private void Update() => CheckForEnemiesInRange();

        public void OnAttack(InputValue value)
        {
            if (value.isPressed) LightAttack();
        }

        public void StrongAttackOnClick() => StrongAttack();

        public void AttackOnClick() => LightAttack();

        private void LightAttack()
        {
            var currentTime = Time.time;
            
            if (!_enemyFinder.IsEnemyInRange(transform))
            {
                if (currentTime - _lastLightAttackTime < _simpleAttackDelay) return;
                _lastLightAttackTime = currentTime;
                OnLightAttack?.Invoke();
                return;
            }
            
            if (currentTime - _lastLightAttackTime < _simpleAttackDelay) return;
            _lastLightAttackTime = currentTime;

            var closestBaseEnemy = _enemyFinder.FindClosestEnemy(transform);
            if (closestBaseEnemy == null) return;

            ExecuteAttack(closestBaseEnemy, _config.Damage);
            OnLightAttack?.Invoke();
            CheckForEnemiesInRange();
        }

        private async void StrongAttack()
        {
            if (IsAttacking) return;

            var currentTime = Time.time;

            if (currentTime - _lastStrongAttackTime < StrongAttackDelay) return;
            if (!_enemyFinder.IsEnemyInRange(transform)) return;

            IsAttacking = true;

            var closestBaseEnemy = _enemyFinder.FindClosestEnemy(transform);
            if (closestBaseEnemy == null)
            {
                IsAttacking = false;
                return;
            }

            ExecuteAttack(closestBaseEnemy, _config.StrongDamage);
            _lastStrongAttackTime = currentTime;
            OnStrongAttack?.Invoke();

            await UniTask.Delay(TimeSpan.FromSeconds(StrongAttackDelay));

            IsAttacking = false;
        }

        private void ExecuteAttack(BaseEnemy enemy, float damage)
        {
            transform.rotation = Quaternion.LookRotation(enemy.transform.position - transform.position);
            enemy.EnemyHealth.TakeDamage(damage);
            _lastStrongAttackTime = Time.time;
        }
        
        private void CheckForEnemiesInRange()
        {
            var isDisabled = _enemyFinder.IsEnemyInRange(transform);
            OnRangeAttackChanged?.Invoke(isDisabled);
        }
    }
}
