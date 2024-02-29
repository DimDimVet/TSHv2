using Pools;
using Zenject;

namespace Bulls
{
    public class EnemyTurnSleeve : Bull
    {
        private IEnemyTurnSleevePoolExecutor enemyBullPool;
        [Inject]
        public void Init(IEnemyTurnSleevePoolExecutor _enemyBullPool)
        {
            enemyBullPool = _enemyBullPool;
        }
        protected override void ReternBullet()
        {
        }
        protected override void ShootSleeve()
        {
            enemyBullPool.ReternObject(this.gameObject.GetHashCode());
        }
        public class Factory : PlaceholderFactory<EnemyTurnSleeve>
        {
        }
    }
}

