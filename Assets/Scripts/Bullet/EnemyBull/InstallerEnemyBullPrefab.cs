using Zenject;

namespace Bulls
{
    public class InstallerEnemyBullPrefab : MonoInstaller
    {
        [Inject] private EnemyBullPrefab enemyBullPrefab;
        public override void InstallBindings()
        {
            Container.BindFactory<EnemyTurn, EnemyTurn.Factory>().FromComponentInNewPrefab(enemyBullPrefab.SetEnemyTurnObject);
            Container.BindFactory<EnemyRif, EnemyRif.Factory>().FromComponentInNewPrefab(enemyBullPrefab.SetEnemyRifObject);
            Container.BindFactory<EnemyTurnSleeve, EnemyTurnSleeve.Factory>().FromComponentInNewPrefab(enemyBullPrefab.SetEnemyTurnSleeveObject);
            Container.BindFactory<EnemyRifSleeve, EnemyRifSleeve.Factory>().FromComponentInNewPrefab(enemyBullPrefab.SetEnemyRifSleeveObject);
        }
    }
}

