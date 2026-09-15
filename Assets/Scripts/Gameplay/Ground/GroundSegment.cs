using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace ZombieRace
{
    public class GroundSegment : MonoBehaviour
    {
        [SerializeField] private Collider segmentBounds;
        [SerializeField] private Collider spawnArea;
        private readonly List<EnemyController> enemies = new List<EnemyController>();

        public Bounds Bounds => this.segmentBounds.bounds;
        public Bounds SpawnArea => this.spawnArea.bounds;
        public IReadOnlyList<EnemyController> Enemies => this.enemies;

        public void AddEnemy(EnemyController enemy)
        { 
            this.enemies.Add(enemy);
            enemy.Died += this.OnEnemyDied;
        }

        public void ClearEnemies()
        {
            this.enemies.ForEach(x => x.Died -= this.OnEnemyDied);
            this.enemies.Clear();
        }

        private void OnEnemyDied(EnemyController enemy)
        {
            enemy.Died -= this.OnEnemyDied;
            this.enemies.Remove(enemy);
        }
    }
}