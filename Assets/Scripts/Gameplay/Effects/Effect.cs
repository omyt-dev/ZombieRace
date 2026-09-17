using System;
using UnityEngine;

namespace ZombieRace
{
    public class Effect : MonoBehaviour
    {
        [SerializeField] private ParticleSystem[] particles;

        private int playingParticles;

        public event Action<Effect> Finished;

        public void Play(Vector3 position)
        {
            this.transform.position = position;
            this.playingParticles = this.particles.Length;

            foreach (ParticleSystem particle in this.particles)
                particle.Play();
        }

        private void Update()
        {
            if (this.playingParticles <= 0)
                return;

            int aliveParticles = 0;
            foreach (ParticleSystem particle in this.particles)
            {
                if (particle.IsAlive(true))
                    aliveParticles++;
            }

            if (aliveParticles > 0)
                return;

            this.playingParticles = 0;
            this.Finished?.Invoke(this);
        }
    }
}
