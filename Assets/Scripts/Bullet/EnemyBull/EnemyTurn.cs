using AudioScene;
using Pools;
using Zenject;

namespace Bulls
{
    public class EnemyTurn : Bull
    {
        private IEnemyTurnPoolExecutor enemyBullPool;
        private IParticleShootEnemyTurnPoolExecutor particle;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IEnemyTurnPoolExecutor _enemyBullPool, IParticleShootEnemyTurnPoolExecutor _particle)
        {
            enemyBullPool = _enemyBullPool;
            particle = _particle;
        }

        protected override void ReternBullet()
        {
            particle.GetObject(gameObject.transform.localScale.x, this.gameObject.transform);
            enemyBullPool.ReternObject(this.gameObject.GetHashCode());
        }
        protected override void ShootSleeve()
        {
        }
        public class Factory : PlaceholderFactory<EnemyTurn>
        {
        }
    }
}

