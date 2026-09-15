using System;

namespace ZombieRace
{
    public class GameSession : IDisposable
    {
        private readonly GameStateMachine stateMaching;
        private readonly GameResetHandler resetHandler;

        public EGameState CurrentState => this.stateMaching.CurrentState;

        public GameSession(GameStateMachine stateMaching, GameResetHandler resetHandler)
        {
            this.stateMaching = stateMaching;
            this.resetHandler = resetHandler;
            //this.stateMaching.StateChanged += OnStateChanged;
        }
        public void Dispose()
        {
            //this.stateMaching.StateChanged -= OnStateChanged;
        }


        public void StartLevel()
        { 
            this.stateMaching.TryChangeState(EGameState.Playing);
        }

        public void FinishLevel(bool win)
        {
            this.stateMaching.TryChangeState(win ? EGameState.Win : EGameState.Lose);
        }
     
        public void RestartLevel()
        {
            this.stateMaching.TryChangeState(EGameState.Ready);
            this.resetHandler.Reset();
        }


        //private void OnStateChanged(EGameState previousState, EGameState newState)
        //{
        //}

    }
}
