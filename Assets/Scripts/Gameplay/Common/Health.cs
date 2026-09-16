using System;
using UnityEngine;

namespace ZombieRace
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public event Action<float, float> HealthChanged;
        public event Action Died;

        public void Initialize(float maxHealth)
        {
            this.MaxHealth = maxHealth;
            this.ResetHealth();
        }

        public void TakeDamage(float amount)
        {
            if (this.CurrentHealth <= 0f || amount <= 0f)
                return;

            this.CurrentHealth = Mathf.Max(this.CurrentHealth - amount,0f);
            this.HealthChanged?.Invoke(this.CurrentHealth, this.MaxHealth);

            if (CurrentHealth > 0f)
                return;

            this.Died?.Invoke();
        }

        public void ResetHealth()
        {
            this.CurrentHealth = this.MaxHealth;
            this.HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }

    }
}
