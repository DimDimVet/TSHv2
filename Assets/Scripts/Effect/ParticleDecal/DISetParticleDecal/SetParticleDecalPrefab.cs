using UnityEngine;
using Zenject;

namespace Effect
{
    [CreateAssetMenu(fileName = "SetParticleDecalPrefab", menuName = "Installers/SetParticleDecalPrefab")]
    public class SetParticleDecalPrefab : ScriptableObjectInstaller<SetParticleDecalPrefab>
    {
        public ParticleDecalPrefab ParticleDecalPrefab;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ParticleDecalPrefab>().FromInstance(ParticleDecalPrefab).AsSingle();
        }
    }
}

