using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class CarController : BaseBehaviour
    {
        private CarMovement movement;
        private Health health;

        public event Action Died;

        private void Awake()
        {
            this.movement = GetComponent<CarMovement>();
            this.health = GetComponent<Health>();

            this.SubscribeEvent(
                () => this.health.Died += OnDied, 
                () => this.health.Died -= OnDied);
        }

        private void OnDied()
        {
            this.movement.StopMoving();
            this.Died?.Invoke();
        }

        protected override void ResetBehaviour()
        {
            this.transform.position = Vector3.zero;
            this.health.ResetHealth();
            this.movement.StopMoving();
        }

        protected override void EnterPlaying()
        {
            this.movement.StartMoving();
        }

        protected override void ExitPlaying()
        {
            this.movement.StopMoving();
        }
    }
}
