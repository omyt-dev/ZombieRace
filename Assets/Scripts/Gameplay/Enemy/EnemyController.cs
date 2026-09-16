using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class EnemyController : BaseBehaviour
    {
        [SerializeField] private EnemyConfig config;

        public event Action<EnemyController> Died;

        private CarController carController;
        private Health carHealth;

        private EnemyStateMachine stateMachine;
        private EnemyAnimatorController enemyAnimator;
        private Health enemyHealth;

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
        private void Construct(CarController carController)
        {
            this.enemyAnimator = this.GetComponent<EnemyAnimatorController>();
            this.stateMachine = new EnemyStateMachine();

            this.enemyHealth = this.GetComponent<Health>();
            this.enemyHealth.Initialize(config.MaxHealth);

            this.carController = carController;
            this.carHealth = carController.GetComponent<Health>();

            this.SubscribeEvent(
                () => this.stateMachine.StateChanged += OnStateChanged,
                () => this.stateMachine.StateChanged -= OnStateChanged);

            this.SubscribeEvent(
                () => this.enemyHealth.Died += OnDied,
                () => this.enemyHealth.Died -= OnDied);
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
            this.CurrentSpeed = Mathf.MoveTowards(this.CurrentSpeed, this.config.MoveSpeed, this.config.Acceleration * Time.deltaTime);
            this.transform.position += direction * (this.CurrentSpeed * Time.deltaTime);
        }

        private void EnterAttack()
        {
            this.carHealth.TakeDamage(this.config.AttackDamage);
            this.stateMachine.TryChangeState(EEnemyState.Dead);
        }

        private void OnDied()
        {
            this.stateMachine.TryChangeState(EEnemyState.Dead);
        }
        private void EnterDead()
        {
            this.Died?.Invoke(this);
        }

        public void ResetEnemy()
        {
            this.enemyAnimator.SetRandomIdle();
            this.enemyHealth.ResetHealth();
            this.stateMachine.Reset();
            this.CurrentSpeed = 0f;
        }
    }
}
