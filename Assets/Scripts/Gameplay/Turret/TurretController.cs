namespace ZombieRace
{
    public class TurretController : BaseBehaviour
    {
        private TurretInput input;
        private TurretAim aim;
        private Weapon weapon;

        private float angle;

        private void Awake()
        {
            this.input = this.GetComponent<TurretInput>();
            this.aim = this.GetComponent<TurretAim>();
            this.weapon = this.GetComponent<Weapon>();
        }

        private void Update()
        {
            if (!this.IsPlaying) 
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
