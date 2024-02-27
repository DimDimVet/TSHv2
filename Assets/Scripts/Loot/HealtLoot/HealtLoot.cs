using Healts;
using Pools;
using Registrator;
using Zenject;

namespace Loot
{
    public class HealtLoot : Loot
    {
        private IHealt healtExecutor;
        private IHealtLootPoolExecutor poolLoot;
        [Inject]
        public void Init(IHealtLootPoolExecutor _poolLoot, IHealt _healtExecutor)
        {
            healtExecutor = _healtExecutor;
            poolLoot = _poolLoot;
        }
        protected override void Executor(Construction player)
        {
            if (player.TypeObject == TypeObject.Player)
            {
                healtExecutor.Healing(player.Hash, healt);
            }
        }
        protected override void ReternLoot()
        {
            poolLoot.ReternObject(this.gameObject.GetHashCode());
        }
        public class Factory : PlaceholderFactory<HealtLoot>
        {
        }
    }
}

