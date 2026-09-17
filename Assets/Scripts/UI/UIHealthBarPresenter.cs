using UnityEngine;

namespace ZombieRace
{
    public class UIHealthBarPresenter : BaseBehaviour
    {
        private Health health;
        private UIHealthBarView view;

        private void Awake()
        {
            this.health = this.GetComponentInParent<Health>();
            this.view = this.GetComponent<UIHealthBarView>();

            this.SubscribeEvent(() => this.health.HealthChanged += OnHealthChanged, () => this.health.HealthChanged -= OnHealthChanged);
        }
       
        protected override void OnEnable()
        {
            base.OnEnable();
            this.view.SetValue(this.health.CurrentHealth, this.health.MaxHealth);
        }

        private void OnHealthChanged(float current, float max)
        {
            this.view.SetValue(current, max);
        }
    }
}
