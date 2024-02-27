using Zenject;

namespace Healts
{
    public class HealtInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IHealt>().To<HealtExecutor>().AsSingle().NonLazy();
        }
    }
}

