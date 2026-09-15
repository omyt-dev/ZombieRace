using System;
using UnityEngine;

namespace ZombieRace
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
        public float CurrentHealth { get; private set; }

        public event Action<float, float> HealthChanged;
        public event Action Died;

        private void Awake()
        {
            this.ResetHealth();
        }

        public void TakeDamage(float amount)
        {
            if (this.CurrentHealth <= 0f || amount <= 0f)
                return;

            this.CurrentHealth = Mathf.Max(this.CurrentHealth - amount,0f);

            StylizedLog.Log($"{this.gameObject.name}:Health", 
                $"Took damage: { StylizedLog.GetColorized(amount.ToString(), StylizedLog.Red)}, " +
                $"Current Health: {StylizedLog.GetColorized(CurrentHealth.ToString(), StylizedLog.Red)}/" +
                    $"{StylizedLog.GetColorized(MaxHealth.ToString(), StylizedLog.Red)}", StylizedLog.Red);

           this.HealthChanged?.Invoke(this.CurrentHealth, this.MaxHealth);

            if (CurrentHealth > 0f)
                return;

            StylizedLog.Log($"{this.gameObject.name}:Health", StylizedLog.Stylize("Died", color: StylizedLog.Purple, bold: true), StylizedLog.Red);
            this.Died?.Invoke();
        }

        public void ResetHealth()
        {
            this.CurrentHealth = this.MaxHealth;
            this.HealthChanged?.Invoke(CurrentHealth, MaxHealth);
        }
    }
}
