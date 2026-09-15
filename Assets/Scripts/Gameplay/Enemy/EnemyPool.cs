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

            item.Died += this.OnEnemyDied;
            item.ResetEnemy();
        }

        protected override void OnDespawned(EnemyController item)
        {
            item.Died -= this.OnEnemyDied;
            base.OnDespawned(item);
        }

        private void OnEnemyDied(EnemyController enemy)
        {
            this.Despawn(enemy);
        }
    }
}
