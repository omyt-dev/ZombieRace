using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;


namespace ZombieRace
{
    public class EffectFactory
    {
        private readonly Dictionary<EEffectType, EffectPool> pools;

        [Inject]
        public EffectFactory(
            [Inject(Id = EEffectType.EnemyHit)] EffectPool hitPool,
            [Inject(Id = EEffectType.CarExplosion)] EffectPool explosionPool)
        {
            this.pools = new Dictionary<EEffectType, EffectPool>
            {
                [EEffectType.EnemyHit] = hitPool,
                [EEffectType.CarExplosion] = explosionPool
            };
        }

        public void Play(EEffectType type, Vector3 position)
        {
            Effect effect = this.pools[type].Spawn();
            effect.Play(position);
        }
    }
}
