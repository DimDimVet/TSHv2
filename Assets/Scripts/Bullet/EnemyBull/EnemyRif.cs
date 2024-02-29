using Pools;
using Zenject;

namespace Bulls
{
    public class EnemyRif : Bull
    {
        private IEnemyRifPoolExecutor enemyBullPool;
        private IParticleShootEnemyRifPoolExecutor particle;
        [Inject]
        public void Init(IEnemyRifPoolExecutor _enemyBullPool, IParticleShootEnemyRifPoolExecutor _particle)
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
        public class Factory : PlaceholderFactory<EnemyRif>
        {
        }
    }
}

