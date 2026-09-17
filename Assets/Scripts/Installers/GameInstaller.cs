using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

namespace ZombieRace
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemyController enemyPrefab;
        [SerializeField] private GroundSegment groundPrefab;
        [SerializeField] private Projectile projectilePrefab;
        [SerializeField] private LevelConfig levelConfig;

        [Header("Effects")]
        [SerializeField] private Effect enemyHitEffectPrefab;
        [SerializeField] private Effect carExplosionEffectPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameResetHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEndHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelProgress>().AsSingle();

            Container.Bind<CarController>().FromComponentInHierarchy().AsSingle();
            Container.BindInstance(this.levelConfig);

            Container.BindMemoryPool<EnemyController, EnemyPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Pool:Enemies");    
            Container.BindMemoryPool<GroundSegment, GroundPool>()
                .WithInitialSize(5)
                .FromComponentInNewPrefab(groundPrefab)
                .UnderTransformGroup("Pool:GroundSegments");
            Container.BindMemoryPool<Projectile, ProjectilePool>()
                .WithInitialSize(20)
                .FromComponentInNewPrefab(projectilePrefab)
                .UnderTransformGroup("Pool:Projectiles");

            Container.Bind<IResetable>().To<ProjectilePool>().FromResolve();

            Container.BindMemoryPool<Effect, EffectPool>()
                .WithId(EEffectType.EnemyHit)
                .WithInitialSize(10)
                .FromComponentInNewPrefab(this.enemyHitEffectPrefab)
                .UnderTransformGroup("Pool:Effects");
            Container.BindMemoryPool<Effect, EffectPool>()
                .WithId(EEffectType.CarExplosion)
                .WithInitialSize(1)
                .FromComponentInNewPrefab(this.carExplosionEffectPrefab)
                .UnderTransformGroup("Pool:Effects");
            Container.Bind<EffectFactory>().AsSingle();
        }
    }
}