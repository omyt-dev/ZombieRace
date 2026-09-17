using System;
using UnityEngine;

namespace ZombieRace
{
    public class Health : MonoBehaviour
    {
        public float MaxHealth { get; private set; }
        public float CurrentHealth { get; private set; }

        public event Action<float, float> HealthChanged;
        public event Action<DamageInfo> Damaged;
        public event Action Died;

        public void Initialize(float maxHealth)
        {
            this.MaxHealth = maxHealth;
            this.ResetHealth();
        }

        public void TakeDamage(DamageInfo info)
        {
            if (this.CurrentHealth <= 0f || info.Amount <= 0f)
                return;

            this.CurrentHealth = Mathf.Max(this.CurrentHealth - info.Amount,0f);
            this.HealthChanged?.Invoke(this.CurrentHealth, this.MaxHealth);
            this.Damaged?.Invoke(info);

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
