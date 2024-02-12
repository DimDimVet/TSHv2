using Zenject;

namespace EnemyLogic
{
    public class ScanEnemyInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IScanEnemyExecutor>().To<ScanEnemyExecutor>().AsSingle().NonLazy();
        }
    }
}

