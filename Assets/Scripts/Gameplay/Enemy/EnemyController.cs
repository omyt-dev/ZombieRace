using System;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class EnemyController : BaseBehaviour
    {
        [SerializeField] private float detectionRange = 10f;
        [SerializeField] private float attackDistance = 1.5f;
        [SerializeField] private float moveSpeed = 3f;
        [SerializeField] private float acceleration = 3f;
        [SerializeField] private float attackDamage = 10f;

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

                float normalizedSpeed = this.moveSpeed > 0f
                    ? this.currentSpeed / this.moveSpeed
                    : 0f;
                this.enemyAnimator.SetNormalizedSpeed(normalizedSpeed);
            }
        }

        [Inject]
        private void Construct(CarController carController)
        {
            this.enemyAnimator = this.GetComponent<EnemyAnimatorController>();
            this.enemyHealth = this.GetComponent<Health>();
            this.stateMachine = new EnemyStateMachine();

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
            StylizedLog.Log(this.gameObject.name + ":EnemyState", $"{previousState} -> {newState}", StylizedLog.Purple);

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
            if (distance <= this.detectionRange)
                this.stateMachine.TryChangeState(EEnemyState.Chase);
        }

        private void EnterChase()
        {
        }
        private void UpdateChase()
        {
            Vector3 direction = this.carController.transform.position - this.transform.position;
            direction.y = 0f;

            float distance = direction.magnitude;
            if (distance <= this.attackDistance)
            {
                this.stateMachine.TryChangeState(EEnemyState.Attack);
                return;
            }

            direction.Normalize();
            this.CurrentSpeed = Mathf.MoveTowards(this.CurrentSpeed, this.moveSpeed, this.acceleration * Time.deltaTime);
            this.transform.position += direction * (this.CurrentSpeed * Time.deltaTime);
            this.transform.rotation = Quaternion.LookRotation(direction);
        }

        private void EnterAttack()
        {
            this.carHealth.TakeDamage(this.attackDamage);
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
            this.enemyHealth.ResetHealth();
            this.stateMachine.Reset();
            this.CurrentSpeed = 0f;
        }
    }
}
