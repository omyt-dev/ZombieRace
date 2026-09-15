using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class GameStateMachine : IInitializable
    {
        public EGameState CurrentState { get; private set; } = EGameState.None;

        public event Action<EGameState, EGameState> StateChanged;

        public void Initialize()
        {
            this.TryChangeState(EGameState.Ready);
        }

        public bool TryChangeState(EGameState newState)
        {
            if (CurrentState == newState)
                return false;

            if (!IsTransitionAllowed(CurrentState, newState))
                return false;

            EGameState previousState = this.CurrentState;
            this.CurrentState = newState;

            StylizedLog.Log("GameState", $"{previousState} -> {newState}", StylizedLog.Blue);

            this.StateChanged?.Invoke(previousState, newState);
            return true;
        }

        private bool IsTransitionAllowed(EGameState currentState, EGameState newState)
        {
            return currentState switch
            {
                EGameState.None => newState == EGameState.Ready,
                EGameState.Ready => newState == EGameState.Playing,
                EGameState.Playing => newState == EGameState.Win || newState == EGameState.Lose,
                EGameState.Win => newState == EGameState.Ready,
                EGameState.Lose => newState == EGameState.Ready,
                _ => false
            };
        }
    }
}