using Zenject;

namespace Bulls
{
    public class InstallerPlayerBullPrefab : MonoInstaller
    {
        [Inject] private PlayerBullPrefab playerBullPrefab;
        public override void InstallBindings()
        {
            Container.BindFactory<PlayerTurn, PlayerTurn.Factory>().FromComponentInNewPrefab(playerBullPrefab.SetTurnObject);
            Container.BindFactory<PlayerRif, PlayerRif.Factory>().FromComponentInNewPrefab(playerBullPrefab.SetRifObject);
            Container.BindFactory<PlayerTurnSleeve, PlayerTurnSleeve.Factory>().FromComponentInNewPrefab(playerBullPrefab.SetTurnSleeveObject);
            Container.BindFactory<PlayerRifSleeve, PlayerRifSleeve.Factory>().FromComponentInNewPrefab(playerBullPrefab.SetRifSleeveObject);
        }
    }
}

