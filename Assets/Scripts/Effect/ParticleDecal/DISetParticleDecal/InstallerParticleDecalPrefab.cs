using Zenject;

namespace Effect
{
    public class InstallerParticleDecalPrefab : MonoInstaller
    {
        [Inject] private ParticleDecalPrefab particleDecalPrefab;
        public override void InstallBindings()
        {
            Container.BindFactory<ParticleRifShootPlayer, ParticleRifShootPlayer.Factory>().FromComponentInNewPrefab(particleDecalPrefab.SetParticleDecalRifPlayer);
            Container.BindFactory<ParticleShootEnemyRif, ParticleShootEnemyRif.Factory>().FromComponentInNewPrefab(particleDecalPrefab.SetParticleDecalShootEnemyRif);
            Container.BindFactory<ParticleShootEnemyTurn, ParticleShootEnemyTurn.Factory>().FromComponentInNewPrefab(particleDecalPrefab.SetParticleDecalShootEnemyTurn);
            Container.BindFactory<ParticleTurnShootPlayer, ParticleTurnShootPlayer.Factory>().FromComponentInNewPrefab(particleDecalPrefab.SetParticleDecalTurnShootPlayer);
        }
    }
}

