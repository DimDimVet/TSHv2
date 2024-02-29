using Pools;
using Zenject;

namespace Bulls
{
    public class EnemyRifSleeve : Bull
    {
        private IEnemyRifSleevePoolExecutor enemyBullPool;
        [Inject]
        public void Init(IEnemyRifSleevePoolExecutor _enemyBullPool)
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
        public class Factory : PlaceholderFactory<EnemyRifSleeve>
        {
        }
    }
}

