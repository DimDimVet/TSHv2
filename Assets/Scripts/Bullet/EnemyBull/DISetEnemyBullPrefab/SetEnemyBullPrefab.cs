using UnityEngine;
using Zenject;

namespace Bulls
{
    [CreateAssetMenu(fileName = "SetEnemyBullPrefab", menuName = "Installers/SetEnemyBullPrefab")]
    public class SetEnemyBullPrefab : ScriptableObjectInstaller<SetEnemyBullPrefab>
    {
        public EnemyBullPrefab EnemyBullPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EnemyBullPrefab>().FromInstance(EnemyBullPrefab).AsSingle();
        }
    }
}

