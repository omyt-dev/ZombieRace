using System;
using UnityEngine;

namespace ZombieRace
{
    [CreateAssetMenu(fileName = "EnemyConfig", menuName = "ZombieRace/Configs/Enemy Config")]
    public class EnemyConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
        [field: SerializeField] public float MoveSpeed { get; private set; } = 3f;
        [field: SerializeField] public float TurnSpeed { get; private set; } = 180f;
        [field: SerializeField] public float Acceleration { get; private set; } = 3f;
        [field: SerializeField] public float DetectionRange { get; private set; } = 10f;
        [field: SerializeField] public float AttackDistance { get; private set; } = 2f;
        [field: SerializeField] public float AttackDamage { get; private set; } = 10f;
        [field: SerializeField] public float DeathReactionDuration { get; internal set; } = 1f;

        [field: Header("Hit Reaction")]
        [field: SerializeField] public float HitReactionAngle { get; private set; } = 20f;
        [field: SerializeField] public float HitReactionDuration { get; private set; } = .15f;
        [field: SerializeField] public float HitReactionSlowing { get; private set; } = 1f;
    }
}
