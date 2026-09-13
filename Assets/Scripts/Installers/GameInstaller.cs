using UnityEngine;
using Zenject; 

namespace ZombieRace
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private EnemyController enemyPrefab;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameStateMachine>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameSession>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameEndHandler>().AsSingle();

            Container.Bind<CarController>().FromComponentInHierarchy().AsSingle();

            Container.BindMemoryPool<EnemyController, EnemyPool>()
                .WithInitialSize(10)
                .FromComponentInNewPrefab(enemyPrefab)
                .UnderTransformGroup("Enemies");    
        }
    }
}