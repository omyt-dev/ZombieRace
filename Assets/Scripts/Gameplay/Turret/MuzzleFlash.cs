using System;
using UnityEngine;

namespace ZombieRace
{
    public class MuzzleFlash : MonoBehaviour
    {
        [SerializeField] private ParticleSystem particles;
        [SerializeField] private Light muzzleLight;
        [SerializeField] private float lightDuration = 0.04f;

        public void Play()
        {
            this.particles.Play();
            this.muzzleLight.enabled = true;

            CancelInvoke(nameof(this.DisableLight));
            Invoke(nameof(this.DisableLight), this.lightDuration);
        }

        private void DisableLight()
        {
            this.muzzleLight.enabled = false;
        }
    }
}
