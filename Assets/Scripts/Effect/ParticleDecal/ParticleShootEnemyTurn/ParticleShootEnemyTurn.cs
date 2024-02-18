using Pools;
using UnityEngine;
using Zenject;

namespace Effect
{
    public class ParticleShootEnemyTurn : ParticleDecal
    {
        [SerializeField, Range(0, 2)] private float defaultKillTime = 0.3f;
        private float killTime;
        private IParticleShootEnemyTurnPoolExecutor particlePool;
        [Inject]
        public void Init(IParticleShootEnemyTurnPoolExecutor _particlePool)
        {
            particlePool = _particlePool;
        }
        private void OnEnable()
        {
            killTime = defaultKillTime;
        }
        private void Update()
        {
            KillTime();
        }
        private void KillTime()
        {
            killTime -= Time.deltaTime;
            if (killTime <= 0)
            {
                particlePool.ReternObject(this.gameObject.GetHashCode());
            }
        }

        public class Factory : PlaceholderFactory<ParticleShootEnemyTurn>
        {
        }
    }
}