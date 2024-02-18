using UnityEngine;
using Zenject;

namespace Bulls
{
    [CreateAssetMenu(fileName = "SetPlayerBullPrefab", menuName = "Installers/SetPlayerBullPrefab")]
    public class SetPlayerBullPrefab : ScriptableObjectInstaller<SetPlayerBullPrefab>
    {
        public PlayerBullPrefab PlayerBullPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<PlayerBullPrefab>().FromInstance(PlayerBullPrefab).AsSingle();
        }
    }
}

