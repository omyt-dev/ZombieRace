using UnityEngine;

namespace ZombieRace
{
    [CreateAssetMenu(fileName = "LevelConfig", menuName = "ZombieRace/Configs/Level Config")]
    public class LevelConfig : ScriptableObject
    {
        [field: Header("Level Settings")]
        [field: SerializeField] public float Length { get; private set; } = 300f;
        [field: SerializeField] public int EnemiesPerSegment { get; private set; } = 15;

        [field: Header("Generation Settings")]
        [field: SerializeField] public float GenerationDistance { get; private set; } = 300f;
        [field: SerializeField] public float DespawnDistance { get; private set; } = 20f;
    }
}