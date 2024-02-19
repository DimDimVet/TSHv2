using AudioScene;
using Pools;
using Zenject;

namespace Bulls
{
    public class PlayerRif : Bull
    {
        private IPlayerRifPoolExecutor playerBullPool;
        private IParticleRifShootPlayerPoolExecutor particle;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerRifPoolExecutor _playerBullPool, IParticleRifShootPlayerPoolExecutor _particle)
        {
            playerBullPool = _playerBullPool;
            particle = _particle;
        }
        protected override void ReternBullet()
        {
            particle.GetObject(gameObject.transform.localScale.x, this.gameObject.transform);
            playerBullPool.ReternObject(this.gameObject.GetHashCode());
        }
        protected override void ShootSleeve()
        {
        }

        public class Factory : PlaceholderFactory<PlayerRif>
        {
        }
    }
}

