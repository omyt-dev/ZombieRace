using System;

namespace ZombieRace
{
    public class EventToken
    {
        private Action subscribe;
        private Action unsubscribe;

        public EventToken(Action subscribe, Action unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Subscribe()
        {
            this.subscribe?.Invoke();
        }

        public void Unsubscribe()
        {
            this.unsubscribe?.Invoke();
        }
    }
}
