using System;
using UnityEngine;

namespace ZombieRace
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private ProjectileConfig config; 
        [SerializeField] private TrailRenderer trail;

        private float timeAlive;

        public event Action<Projectile> Expired;

        public void Launch(Vector3 position, Vector3 direction)
        {
            this.trail.enabled = false;

            this.transform.position = position;
            this.transform.forward = direction.normalized;

            this.trail.Clear();
            this.trail.enabled = true;

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

            Vector3 hitPoint = other.ClosestPoint(this.transform.position);

            health.TakeDamage(DamageInfo.Detailed(this.config.Damage, this.transform.forward, hitPoint));
            this.Expired?.Invoke(this);
        }
    }
}
