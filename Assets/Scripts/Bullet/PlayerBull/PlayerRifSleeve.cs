using Pools;
using Zenject;

namespace Bulls
{
    public class PlayerRifSleeve : Bull
    {
        private IPlayerRifSleevePoolExecutor playerBullPool;
        [Inject]
        public void Init(IPlayerRifSleevePoolExecutor _playerBullPool)
        {
            playerBullPool = _playerBullPool;
        }
        protected override void ReternBullet()
        {

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

