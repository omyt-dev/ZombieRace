using UnityEngine;

namespace ZombieRace
{
    public class EnemyAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int NormalizedSpeed = Animator.StringToHash("NormalizedSpeed");

        public void SetNormalizedSpeed(float speed)
        { 
            this.animator.SetFloat(NormalizedSpeed, speed);
        }
    }
}
