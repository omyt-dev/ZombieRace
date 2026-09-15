using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public abstract class BaseBehaviour : MonoBehaviour
    {
        private GameStateMachine gameStateMachine;
        private readonly List<EventToken> eventTokens = new List<EventToken>();

        protected bool IsPlaying { get; private set; }

        [Inject]
        private void Construct(GameStateMachine gameStateMachine)
        {
            this.gameStateMachine = gameStateMachine;

            this.SubscribeEvent(
              () => this.gameStateMachine.StateChanged += this.OnGameStateChanged,
              () => this.gameStateMachine.StateChanged -= this.OnGameStateChanged);

            this.OnGameStateChanged(EGameState.None, gameStateMachine.CurrentState);
        }

#region EventHandling
        protected EventToken SubscribeEvent(Action subscribe, Action unsubscribe)
        {
            var token = new EventToken(subscribe, unsubscribe);
            this.eventTokens.Add(token);
            return token;
        }

        protected void UnsubscribeEvent(EventToken token)
        {
            token.Unsubscribe();
            this.eventTokens.Remove(token);
        }

        protected virtual void OnEnable()
        {
            this.eventTokens.ForEach(x => x.Subscribe());
        }
        protected virtual void OnDisable()
        {
            this.eventTokens.ForEach(x => x.Unsubscribe());
        }
#endregion

#region GameStateHandling
        private void OnGameStateChanged(EGameState previousState, EGameState newState)
        {
            switch (previousState)
            {
                case EGameState.Ready:
                    this.ExitReady();
                    break;

                case EGameState.Playing:
                    this.IsPlaying = false;
                    this.ExitPlaying();
                    break;

                case EGameState.Win:
                    this.ExitWin();
                    break;

                case EGameState.Lose:
                    this.ExitLose();
                    break;
            }

            switch (newState)
            {
                case EGameState.Ready:
                    this.ResetBehaviour();
                    this.EnterReady();
                    break;

                case EGameState.Playing:
                    this.IsPlaying = true;
                    this.EnterPlaying();
                    break;

                case EGameState.Win:
                    this.EnterWin();
                    break;

                case EGameState.Lose:
                    this.EnterLose();
                    break;
            }

            this.GameStateChanged(previousState, newState);
        }

        protected virtual void GameStateChanged(EGameState previousState, EGameState newState) { }

        protected virtual void EnterReady() { }
        protected virtual void EnterPlaying() { }
        protected virtual void EnterLose() { }
        protected virtual void EnterWin() { }

        protected virtual void ExitReady() { }
        protected virtual void ExitPlaying() { }
        protected virtual void ExitLose() { }
        protected virtual void ExitWin() { }

        protected virtual void ResetBehaviour() { }
#endregion
    }
}
