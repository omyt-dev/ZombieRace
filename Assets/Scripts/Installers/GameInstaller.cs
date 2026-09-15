using UnityEngine;
using Zenject; 

namespace ZombieRace
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemyController enemyPrefab;
        [SerializeField] private GroundSegment groundPrefab;
        [SerializeField] private Projectile projectilePrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameResetHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEndHandler>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelProgress>().AsSingle();

            Container.Bind<CarController>().FromComponentInHierarchy().AsSingle();

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
        }
    }
}