using UnityEngine;

namespace ZombieRace
{
    [CreateAssetMenu(fileName = "ProjectileConfig", menuName = "ZombieRace/Configs/Projectile Config")]
    public class ProjectileConfig : ScriptableObject
    {
        [field: SerializeField] public float Speed { get; private set; } = 30f;
        [field: SerializeField] public float Damage { get; private set; } = 40f;
        [field: SerializeField] public float Lifetime { get; private set; } = 5f;
    }
}