using UnityEngine;
using Zenject;

namespace Loot
{
    [CreateAssetMenu(fileName = "SetHealtLootPrefab", menuName = "Installers/SetHealtLootPrefab")]
    public class SetHealtLootPrefab : ScriptableObjectInstaller<SetHealtLootPrefab>
    {
        public HealtLootPrefab HealtLootPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<HealtLootPrefab>().FromInstance(HealtLootPrefab).AsSingle();
        }
    }
}

