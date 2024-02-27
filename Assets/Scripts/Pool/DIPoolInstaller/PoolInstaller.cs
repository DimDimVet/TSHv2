using Zenject;

namespace Pools
{
    public class PoolInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IPlayerTurnPoolExecutor>().To<PlayerTurnPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IPlayerRifPoolExecutor>().To<PlayerRifPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IPlayerTurnSleevePoolExecutor>().To<PlayerTurnSleevePoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IPlayerRifSleevePoolExecutor>().To<PlayerRifSleevePoolExecutor>().AsSingle().NonLazy();

            Container.Bind<IEnemyTurnPoolExecutor>().To<EnemyTurnPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IEnemyRifPoolExecutor>().To<EnemyRifPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IEnemyTurnSleevePoolExecutor>().To<EnemyTurnSleevePoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IEnemyRifSleevePoolExecutor>().To<EnemyRifSleevePoolExecutor>().AsSingle().NonLazy();

            Container.Bind<IParticleRifShootPlayerPoolExecutor>().To<ParticleRifShootPlayerPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IParticleShootEnemyRifPoolExecutor>().To<ParticleShootEnemyRifPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IParticleShootEnemyTurnPoolExecutor>().To<ParticleShootEnemyTurnPoolExecutor>().AsSingle().NonLazy();
            Container.Bind<IParticleTurnShootPlayerPoolExecutor>().To<ParticleTurnShootPlayerPoolExecutor>().AsSingle().NonLazy();

            Container.Bind<IHealtLootPoolExecutor>().To<HealtLootPoolExecutor>().AsSingle().NonLazy();

        }
    }
}

