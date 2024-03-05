using System;
using Common;
using Enemy.Enum;
using Enemy.StateMachine.States;
using UnityEngine;

namespace Enemy
{
    public abstract class BaseEnemy : MonoBehaviour
    {
        public event Action<BaseEnemy> OnDeath;
        
        public VisibleStateProvider VisibleStateProvider => _visibleStateProvider;
        public Health EnemyHealth => _enemyHealth;
        public abstract EnemyType EnemyType { get; }

        [SerializeField] private StateMachine.StateMachine _stateMachine;
        [SerializeField] private Health _enemyHealth;
        [SerializeField] private VisibleStateProvider _visibleStateProvider;

        private void Start() => _stateMachine.ChangeState<IdleState>();

        public void Die() => OnDeath?.Invoke(this);

        public void ClearCallbacks()
        {
            _visibleStateProvider.ClearCallbacks();
            OnDeath = null;
        }
    }
}