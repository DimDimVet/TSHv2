using Zenject;

namespace UI
{
    public class IUPanelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IUIPanelsExecutor>().To<UIPanelsExecutor>().AsSingle().NonLazy();
        }
    }
}

