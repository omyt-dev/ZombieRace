using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace ZombieRace
{
    public class EnemyController : BaseBehaviour
    {
        [SerializeField] private EnemyConfig config;

        public event Action<EnemyController> Died;

        private EffectFactory effectFactory;
        private CarController carController;
        private Health carHealth;

        private EnemyStateMachine stateMachine;
        private EnemyAnimatorController enemyAnimator;
        private EnemyHitReaction enemyHitReaction;
        private Health enemyHealth;
        private HitFlash hitFlash;
        
        private float currentSpeed = 0f;

        private float CurrentSpeed
        { 
            get => this.currentSpeed;
            set
            {
                this.currentSpeed = value;

                float normalizedSpeed = this.config.MoveSpeed > 0f
                    ? this.currentSpeed / this.config.MoveSpeed
                    : 0f;
                this.enemyAnimator.SetNormalizedSpeed(normalizedSpeed);
            }
        }

        [Inject]
        private void Construct(CarController carController, EffectFactory effectFactory)
        {
            this.enemyAnimator = this.GetComponent<EnemyAnimatorController>();
            this.hitFlash = this.GetComponent<HitFlash>();
            this.stateMachine = new EnemyStateMachine();

            this.enemyHealth = this.GetComponent<Health>();
            this.enemyHealth.Initialize(this.config.MaxHealth);

            this.enemyHitReaction = this.GetComponent<EnemyHitReaction>();
            this.enemyHitReaction.Initialize(this.config.HitReactionAngle, 
                this.config.HitReactionDuration);

            this.effectFactory = effectFactory;
            this.carController = carController;
            this.carHealth = carController.GetComponent<Health>();

            this.SubscribeEvent(
                () => this.stateMachine.StateChanged += OnStateChanged,
                () => this.stateMachine.StateChanged -= OnStateChanged);

            this.SubscribeEvent(
                () => this.enemyHealth.Died += OnDied,
                () => this.enemyHealth.Died -= OnDied);

            this.SubscribeEvent(
                () => this.enemyHealth.Damaged += this.OnDamaged,
                () => this.enemyHealth.Damaged -= this.OnDamaged);
        }

        private void OnStateChanged(EEnemyState previousState, EEnemyState newState)
        {
            switch (newState)
            {
                case EEnemyState.Idle:
                    this.EnterIdle();
                    break;

                case EEnemyState.Chase:
                    this.EnterChase();
                    break;

                case EEnemyState.Attack:
                    this.EnterAttack();
                    break;

                case EEnemyState.Dead:
                    this.EnterDead();
                    break;
            }
        }

        private void Update()
        {
            //if (!this.IsPlaying)
            //    return;

            switch (this.stateMachine.CurrentState)
            {
                case EEnemyState.Idle:
                    this.UpdateIdle();
                    break;

                case EEnemyState.Chase:
                    this.UpdateChase();
                    break;
            }
        }

        //protected override void ExitPlaying()
        //{
        //    this.stateMachine.TryChangeState(EEnemyState.Idle);
        //}

        private void OnDamaged(DamageInfo info)
        {
            this.CurrentSpeed -= this.config.HitReactionSlowing;
            this.enemyHitReaction.Play(info.Direction);
            this.effectFactory.Play(EEffectType.EnemyHit, info.HitPoint);
            this.hitFlash.Play();
        }

        private void EnterIdle() { this.CurrentSpeed = 0f; }
        private void UpdateIdle()
        {
            float distance = Vector3.Distance(this.transform.position, this.carController.transform.position);
            if (distance <= this.config.DetectionRange)
                this.stateMachine.TryChangeState(EEnemyState.Chase);
        }

        private void EnterChase()
        {
        }
        private void UpdateChase()
        {
            Vector3 direction = (this.carController.transform.position - this.transform.position).FlatY();

            float distance = direction.magnitude;
            if (distance <= this.config.AttackDistance)
            {
                this.stateMachine.TryChangeState(EEnemyState.Attack);
                return;
            }

            if (direction.sqrMagnitude > 0.001f)
            { 
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, targetRotation, this.config.TurnSpeed * Time.deltaTime);
            }

            direction.Normalize();

            float targetSpeed = this.carHealth.CurrentHealth == 0 || !this.IsPlaying ? 0 : this.config.MoveSpeed;
            this.CurrentSpeed = Mathf.MoveTowards(this.CurrentSpeed, targetSpeed, this.config.Acceleration * Time.deltaTime);
            this.transform.position += direction * (this.CurrentSpeed * Time.deltaTime);
        }

        private void EnterAttack()
        {
            this.carHealth.TakeDamage(DamageInfo.Simple(this.config.AttackDamage));
            this.enemyHealth.TakeDamage(DamageInfo.Detailed(this.enemyHealth.CurrentHealth, -this.transform.forward, 
                this.transform.position + Vector3.up));
        }

        private void OnDied()
        {
            this.stateMachine.TryChangeState(EEnemyState.Dead);
        }
        private void EnterDead()
        {
            this.currentSpeed = 0f;
            this.StartCoroutine(this.DeathSequence());
        }

        private IEnumerator DeathSequence()
        {
            yield return new WaitForSeconds(this.config.DeathReactionDuration);
            this.Died?.Invoke(this);
        }

        public void ResetEnemy()
        {
            this.enemyAnimator.SetRandomIdle();
            this.enemyHealth.ResetHealth();
            this.hitFlash.ResetFlash();
            this.stateMachine.Reset();
            this.CurrentSpeed = 0f;
        }
    }
}
