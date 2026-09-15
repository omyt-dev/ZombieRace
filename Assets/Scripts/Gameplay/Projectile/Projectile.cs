using System;
using UnityEngine;

namespace ZombieRace
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 20f;
        [SerializeField] private float damage = 25f;
        [SerializeField] private float lifetime = 3f;

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
            this.transform.Translate(Vector3.forward * speed * Time.deltaTime);
            this.timeAlive += Time.deltaTime;

            if (this.timeAlive >= this.lifetime)
                this.Expired?.Invoke(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            Health health = other.GetComponentInParent<Health>();

            if (health == null)
                return;

            health.TakeDamage(this.damage);
            this.Expired?.Invoke(this);
        }
    }
}
