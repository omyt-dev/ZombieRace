using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class CarController : BaseBehaviour
    {
        [SerializeField] private GameObject visual;
        [SerializeField] private List<TrailRenderer> trails;
        [SerializeField] private CarConfig config;
        
        private CarMovement movement;
        private CarHitReaction hitReaction;
        private HitFlash hitFlash;
        private Health health;

        private EffectFactory effectFactory;

        public event Action Died;

        [Inject]
        private void Construct(EffectFactory effectFactory)
        {
            this.movement = this.GetComponent<CarMovement>();
            this.health = this.GetComponent<Health>();
            this.hitReaction = this.GetComponent<CarHitReaction>();
            this.hitFlash = this.GetComponent<HitFlash>();

            this.effectFactory = effectFactory;

            this.movement.Initialize(this.config.Speed, this.config.AngleCurve, this.config.AngleMax,
                this.config.SegmentDistanceMin, this.config.SegmentDistanceMax);
            this.health.Initialize(this.config.MaxHealth);

            this.SubscribeEvent(
                () => this.health.Died += OnDied, 
                () => this.health.Died -= OnDied);

            this.SubscribeEvent(
                () => this.health.Damaged += this.OnDamaged,
                () => this.health.Damaged -= this.OnDamaged);
        }

        private void OnDamaged(DamageInfo info)
        {
            this.hitReaction.Play();
            this.hitFlash.Play();
        }

        private void OnDied()
        {
            this.movement.StopMoving();
            this.effectFactory.Play(EEffectType.CarExplosion, this.transform.position + Vector3.up);
            this.visual.SetActive(false);

            this.StartCoroutine(this.DeathSequence());
        }

        private IEnumerator DeathSequence()
        {
            yield return new WaitForSeconds(this.config.DeathDelay);
            this.Died?.Invoke();
        }

        protected override void ResetBehaviour()
        {
            this.trails.ForEach(x => x.enabled = false);
            this.transform.position = Vector3.zero;

            this.trails.ForEach(x =>
            {
                x.Clear();
                x.enabled = true;
            });

            this.health.ResetHealth();
            this.movement.StopMoving();
            this.hitReaction.ResetReaction();
            this.hitFlash.ResetFlash(); 
            this.visual.SetActive(true);
        }

        protected override void EnterPlaying()
        {
            this.movement.StartMoving();
        }

        protected override void ExitPlaying()
        {
            this.movement.StopMoving();
        }
    }
}
