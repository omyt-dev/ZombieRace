using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class CarController : EventBehaviour
    {
        private GameStateMachine gameStateMachine;
        private CarMovement movement;
        private Health health;

        public event Action Died;

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;
            this.movement = GetComponent<CarMovement>();
            this.health = GetComponent<Health>();

            this.SubscribeEvent(
                () => this.gameStateMachine.StateChanged += OnGameStateChanged, 
                () => this.gameStateMachine.StateChanged -= OnGameStateChanged);

            this.SubscribeEvent(
                () => this.health.Died += OnDied, 
                () => this.health.Died -= OnDied);
        }

        private void OnDied()
        {
            this.movement.StopMoving();
            this.Died?.Invoke();
        }

        private void OnGameStateChanged(EGameState previousState, EGameState newState)
        {
            switch (newState)
            {
                case EGameState.Playing:
                    this.movement.StartMoving();
                    break;
                default:
                    this.movement.StopMoving();
                    break;
            }
        }
    }
}
