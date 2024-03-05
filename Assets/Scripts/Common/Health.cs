using System;
using ScriptableObjects;
using UnityEngine;

namespace Common
{
    public class Health : MonoBehaviour, IHealth
    {
        public event Action OnHealthZero;
        public event Action<float> OnHealthChanged;

        [field: SerializeField] public float CurrentHealth { get; private set; }
        [SerializeField] private BaseDataConfig _baseData;
        private int _health;
        
        private void Start()
        {
            _health = _baseData.Health;
            CurrentHealth = _health;
        }

        public void TakeDamage(float damage)
        {
            if (CurrentHealth <= 0) return;

            CurrentHealth -= damage;
            OnHealthChanged?.Invoke(damage);

            if (CurrentHealth <= 0) OnHealthZero?.Invoke();
        }

        public void Heal(float healAmount)
        {
            CurrentHealth += healAmount;
            OnHealthChanged?.Invoke(healAmount);
        }
    }
}