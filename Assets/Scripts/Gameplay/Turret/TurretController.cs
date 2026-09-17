using UnityEngine;

namespace ZombieRace
{
    public class TurretController : BaseBehaviour
    {
        [SerializeField] private GameObject visual;
        [SerializeField] private TurretConfig config;

        private TurretInput input;
        private TurretAim aim;
        private Weapon weapon;

        private float angle;

        private void Awake()
        {
            this.input = this.GetComponent<TurretInput>();
            this.aim = this.GetComponent<TurretAim>();
            this.weapon = this.GetComponent<Weapon>();

            this.input.SetSensetivity(config.Sensitivity);
            this.aim.Initialize(config.RotationSpeed, config.MinAngle, config.MaxAngle);
            this.weapon.Initialize(config.FireRate);
        }

        private void Update()
        {
            if (!this.IsPlaying || !visual.activeSelf) 
                return;

            this.angle += this.input.GetInputDelta();
            this.angle = this.aim.AimAt(this.angle);
            this.weapon.TryFire();
        }

        protected override void ResetBehaviour()
        {
            this.angle = 0;
            this.aim.ResetAim();
            this.weapon.ResetWeapon();
        }
    }
}
