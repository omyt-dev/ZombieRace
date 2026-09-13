using System;
using System.Collections.Generic;
using System.Text;
using Zenject;

namespace ZombieRace
{
    public class EnemyPool : MonoMemoryPool<EnemyController>
    {
        protected override void OnCreated(EnemyController item)
        {
            base.OnCreated(item);
        }

        protected override void OnSpawned(EnemyController item)
        {
            base.OnSpawned(item);
        }

        protected override void OnDespawned(EnemyController item)
        {
            base.OnDespawned(item);
        }

    }
}
