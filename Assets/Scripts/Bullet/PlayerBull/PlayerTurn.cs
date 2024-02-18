using AudioScene;
using Pools;
using Zenject;

namespace Bulls
{
    public class PlayerTurn : Bull
    {
        private IPlayerTurnPoolExecutor playerBullPool;
        private IParticleTurnShootPlayerPoolExecutor particle;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerTurnPoolExecutor _playerBullPool, IParticleTurnShootPlayerPoolExecutor _particle)
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
        public class Factory : PlaceholderFactory<PlayerTurn>
        {
        }
    }
}

