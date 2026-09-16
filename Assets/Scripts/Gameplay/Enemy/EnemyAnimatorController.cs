using UnityEngine;

namespace ZombieRace
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;
        [SerializeField] private int idleCount;
        [SerializeField] private bool idleValueRound;

        private static readonly int NormalizedSpeed = Animator.StringToHash("NormalizedSpeed");
        private static readonly int IdleVariant = Animator.StringToHash("IdleVariant");

        public void SetNormalizedSpeed(float speed)
        { 
            this.animator.SetFloat(NormalizedSpeed, speed);
        }

        public void SetRandomIdle()
        {
            float value = Random.Range(0f, idleCount);

            if (idleValueRound)
                value = Mathf.Round(value);

            this.animator.SetFloat(IdleVariant, value);
        }
    }
}
