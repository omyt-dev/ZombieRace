using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class EffectPool : MonoMemoryPool<Effect>
    {
        protected override void OnSpawned(Effect effect)
        {
            base.OnSpawned(effect);
            effect.Finished += this.OnEffectFinished;
        }

        protected override void OnDespawned(Effect effect)
        {
            effect.Finished -= this.OnEffectFinished;
            base.OnDespawned(effect);
        }

        private void OnEffectFinished(Effect effect)
        {
            this.Despawn(effect);
        }
    }
}
