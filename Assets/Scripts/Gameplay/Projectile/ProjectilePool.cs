using System;
using System.Collections.Generic;
using Zenject;

namespace ZombieRace
{
    public class ProjectilePool : MonoMemoryPool<Projectile>, IResetable
    {
        private readonly List<Projectile> activeProjectiles = new();

        protected override void OnSpawned(Projectile item)
        {
            base.OnSpawned(item);
            this.activeProjectiles.Add(item);
            item.Expired += OnProjectileExpired;
        }

        protected override void OnDespawned(Projectile item)
        {
            item.Expired -= OnProjectileExpired;
            this.activeProjectiles.Remove(item);
            base.OnDespawned(item);
        }

        private void OnProjectileExpired(Projectile projectile)
        {
            this.Despawn(projectile);
        }

        public void Reset()
        {
            while (this.activeProjectiles.Count > 0)
                this.Despawn(this.activeProjectiles[0]);
        }
    }
}
