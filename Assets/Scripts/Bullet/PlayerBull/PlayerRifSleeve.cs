using AudioScene;
using Pools;
using Zenject;

namespace Bulls
{
    public class PlayerRifSleeve : Bull
    {
        private IPlayerRifSleevePoolExecutor playerBullPool;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerRifSleevePoolExecutor _playerBullPool)
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
        public class Factory : PlaceholderFactory<PlayerRifSleeve>
        {
        }
    }
}

