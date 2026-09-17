using UnityEngine;

namespace ZombieRace
{
    [CreateAssetMenu(fileName = "CarConfig", menuName = "ZombieRace/Configs/Car Config")]
    public class CarConfig : ScriptableObject
    {
        [field: SerializeField] public float MaxHealth { get; private set; } = 100f;
        [field: SerializeField] public float Speed { get; private set; } = 5f;
        [field: SerializeField] public float DeathDelay { get; private set; } = 1f;

        [field: Header("Movement Randomization")]
        [field: SerializeField] public AnimationCurve AngleCurve { get; private set; }
        [field: SerializeField] public float AngleMax { get; private set; } = 5f;
        [field: SerializeField] public float SegmentDistanceMax { get; private set; } = 300f;
        [field: SerializeField] public float SegmentDistanceMin { get; private set; } = 100f;
    }
}
