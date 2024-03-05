using System;

namespace Common
{
    public interface IHealth
    {
        event Action OnHealthZero;
        event Action<float> OnHealthChanged;

        float CurrentHealth { get; }
        void TakeDamage(float damage);
        void Heal(float healAmount);
    }
}