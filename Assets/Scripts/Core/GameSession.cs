using System;

namespace ZombieRace
{
    public class GameSession : IDisposable
    {
        private readonly GameStateMachine stateMaching;

        public EGameState CurrentState => this.stateMaching.CurrentState;

        public GameSession(GameStateMachine stateMaching)
        {
            this.stateMaching = stateMaching;
            this.stateMaching.StateChanged += OnStateChanged;
        }
        public void Dispose()
        {
            this.stateMaching.StateChanged -= OnStateChanged;
        }


        public void StartLevel()
        { 
            this.stateMaching.TryChangeState(EGameState.Playing);
        }

        public void FinishLevel(bool win)
        {
            this.stateMaching.TryChangeState(
                win ? EGameState.Win : EGameState.Lose);
        }
     
        public void RestartLevel()
        {
            this.stateMaching.TryChangeState(EGameState.Ready);
        }


        private void OnStateChanged(EGameState previousState, EGameState newState)
        {
        }

    }
}
