using System;

namespace ZombieRace
{
    public class EnemyStateMachine
    {
        public EEnemyState CurrentState { get; private set; } = EEnemyState.Idle;

        public event Action<EEnemyState, EEnemyState> StateChanged;

        public bool TryChangeState(EEnemyState newState)
        {
            if (CurrentState == newState)
                return false;

            EEnemyState previousState = this.CurrentState;
            this.CurrentState = newState;

            this.StateChanged?.Invoke(previousState, newState);
            return true;
        }

        public void Reset()
        {
            this.CurrentState = EEnemyState.Idle;
        }
    }
}
