using UnityEngine;

namespace ZombieRace
{
    [CreateAssetMenu(fileName = "TurretConfig", menuName = "ZombieRace/Configs/Turret Config")]
    public class TurretConfig : ScriptableObject
    {
        [Header("Aim")]
        [field: SerializeField] public float RotationSpeed { get; private set; } = 360f;
        [field: SerializeField] public float MinAngle { get; private set; } = -60f;
        [field: SerializeField] public float MaxAngle { get; private set; } = 60f;

        [Header("Input")]
        [field: SerializeField] public float Sensitivity { get; private set; } = .1f;

        [Header("Weapon")]
        [field: SerializeField] public float FireRate { get; private set; } = 3f;
    }
}