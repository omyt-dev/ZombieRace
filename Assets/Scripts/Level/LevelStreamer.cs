using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

namespace ZombieRace
{
    public class LevelStreamer : BaseBehaviour
    {
        [SerializeField] private float generationDistance = 200f;
        [SerializeField] private float despawnDistance = 20f;
        [SerializeField] private float enemiesPerSegment = 4f;

        private float nextSegmentPosition;

        private readonly Queue<GroundSegment> segments = new Queue<GroundSegment>();

        private CarController carController;
        private GroundPool groundPool;
        private EnemyPool enemyPool;

        [Inject]
        private void Construct(CarController car, GroundPool groundPool, EnemyPool enemyPool)
        {
            this.carController = car;
            this.groundPool = groundPool;
            this.enemyPool = enemyPool;
        }

        private void Update()
        {
            this.SpawnSegments();
            this.DespawnSegments();
        }

        private void SpawnSegments()
        {
            float spawnThreshold = this.carController.transform.position.z + this.generationDistance;

            while (this.nextSegmentPosition < spawnThreshold)
                this.SpawnSegment();
        }

        private void SpawnSegment()
        {
            GroundSegment segment = this.groundPool.Spawn();
            segment.transform.position = Vector3.forward * nextSegmentPosition;

            this.nextSegmentPosition = segment.transform.position.z + segment.Bounds.size.z;

            if(segments.Count > 0)
                this.SpawnEnemies(segment);
            this.segments.Enqueue(segment);
        }

        private void SpawnEnemies(GroundSegment segment)
        {
            for (int i = 0; i < this.enemiesPerSegment; i++)
            {
                EnemyController enemy = this.enemyPool.Spawn();

                Vector3 position = this.GetRandomEnemyPosition(segment);
                enemy.transform.position = position;
                segment.AddEnemy(enemy);
            }
        }

        private Vector3 GetRandomEnemyPosition(GroundSegment segment)
        {
            var bounds = segment.SpawnArea;

            float x = Random.Range(-bounds.extents.x, bounds.extents.x);
            float z = Random.Range(-bounds.extents.z, bounds.extents.z);

            return segment.transform.position + new Vector3(x, segment.SpawnArea.max.y, z);
        }

        private void DespawnSegments()
        {
            float despawnThreshold = this.carController.transform.position.z - this.despawnDistance;

            while (this.segments.Count > 0)
            {
                GroundSegment segment = this.segments.Peek();
                if (segment.Bounds.max.z > despawnThreshold)
                    break;

                this.DespawnEnemies(segment);
                this.groundPool.Despawn(segment);
                this.segments.Dequeue();
            }
        }

        private void DespawnEnemies(GroundSegment segment)
        {
            foreach (EnemyController enemy in segment.Enemies)
                this.enemyPool.Despawn(enemy);

            segment.ClearEnemies();
        }

        protected override void ResetBehaviour()
        {
            while (this.segments.Count > 0)
            {
                var segment = this.segments.Dequeue();

                this.DespawnEnemies(segment);
                this.groundPool.Despawn(segment);
            }

            this.nextSegmentPosition = 0f;
            this.SpawnSegments();
        }
    }
}
