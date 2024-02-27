using Zenject;

namespace Loot
{
    public class LootPrefabInstaller : MonoInstaller
    {
        [Inject] private HealtLootPrefab healtLootPrefab;
        public override void InstallBindings()
        {
            Container.BindFactory<HealtLoot, HealtLoot.Factory>().FromComponentInNewPrefab(healtLootPrefab.SetObject);
        }
    }
}

