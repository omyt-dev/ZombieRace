using System;
using UnityEngine;

namespace ZombieRace
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig config;

        private float timeAlive;

        public event Action<Projectile> Expired;

        public void Launch(Vector3 position, Vector3 direction)
        {
            this.transform.position = position;
            this.transform.forward = direction.normalized;
            this.timeAlive = 0f;
        }

        private void Update()
        {
            this.transform.Translate(Vector3.forward * this.config.Speed * Time.deltaTime);
            this.timeAlive += Time.deltaTime;

            if (this.timeAlive >= this.config.Lifetime)
                this.Expired?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponentInParent<Health>();

            if (health == null)
                return;

            health.TakeDamage(this.config.Damage);
            this.Expired?.Invoke(this);
        }
    }
}
