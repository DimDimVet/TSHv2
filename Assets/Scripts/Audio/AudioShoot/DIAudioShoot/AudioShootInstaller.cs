using Zenject;

namespace AudioScene
{
    public class AudioShootInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAudioShootExecutor>().To<AudioShootExecutor>().AsSingle().NonLazy();
        }
    }
}