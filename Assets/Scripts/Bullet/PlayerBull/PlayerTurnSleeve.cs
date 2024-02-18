using AudioScene;
using Pools;
using Zenject;

namespace Bulls
{
    public class PlayerTurnSleeve : Bull
    {
        private IPlayerTurnSleevePoolExecutor playerBullPool;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerTurnSleevePoolExecutor _playerBullPool)
        {
            playerBullPool = _playerBullPool;
        }
        protected override void ReternBullet()
        {
            //particlePool.GetObject(playerBull.DirectionPlayer(), this.gameObject.transform);
            
        }
        protected override void ShootSleeve()
        {
            playerBullPool.ReternObject(this.gameObject.GetHashCode());
        }
        public class Factory : PlaceholderFactory<PlayerTurnSleeve>
        {
        }
    }
}

