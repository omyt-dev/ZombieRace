using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform muzzle;
        [SerializeField] private MuzzleFlash muzzleFlash;
         
        private ProjectilePool projectilePool;
        private float fireRate = 5f;
        private float nextFireTime;

        [Inject]
        private void Construct(ProjectilePool projectilePool)
        {
            this.projectilePool = projectilePool;
        }

        public void Initialize(float fireRate)
        {
            this.fireRate = fireRate;
        }

        public bool TryFire()
        {
            if (Time.time < this.nextFireTime)
                return false;

            this.Fire();
            this.nextFireTime = Time.time + 1f / this.fireRate;
            return true;
        }

        private void Fire()
        {
            this.muzzleFlash.Play();
            this.projectilePool.Spawn().Launch(this.muzzle.position, this.muzzle.forward);
        }

        public void ResetWeapon()
        {
            this.nextFireTime = 0;
        }
    }
}
