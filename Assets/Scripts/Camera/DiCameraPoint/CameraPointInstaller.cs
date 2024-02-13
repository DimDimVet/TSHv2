using Zenject;

namespace CameraMain
{
    public class CameraPointInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<ICameraPointExecutor>().To<CameraPointExecutor>().AsSingle().NonLazy();
        }
    }
}

