using Pools;
using Zenject;

namespace Bulls
{
    public class PlayerTurnSleeve : Bull
    {
        private IPlayerTurnSleevePoolExecutor playerBullPool;
        [Inject]
        public void Init(IPlayerTurnSleevePoolExecutor _playerBullPool)
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
        public class Factory : PlaceholderFactory<PlayerTurnSleeve>
        {
        }
    }
}

