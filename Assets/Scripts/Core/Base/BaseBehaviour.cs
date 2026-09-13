using System;
using System.Collections.Generic;
using UnityEngine;

namespace ZombieRace
{
    public abstract class EventBehaviour : MonoBehaviour
    {
        private List<EventToken> eventTokens = new List<EventToken>();

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
    }
}
