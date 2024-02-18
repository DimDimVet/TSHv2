using AudioScene;
using Pools;
using Zenject;

namespace Bulls
{
    public class EnemyTurnSleeve : Bull
    {
        private IEnemyTurnSleevePoolExecutor enemyBullPool;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IEnemyTurnSleevePoolExecutor _enemyBullPool)
        {
            enemyBullPool = _enemyBullPool;
        }
        protected override void ReternBullet()
        {
            //particlePool.GetObject(playerBull.DirectionPlayer(), this.gameObject.transform);
            
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

