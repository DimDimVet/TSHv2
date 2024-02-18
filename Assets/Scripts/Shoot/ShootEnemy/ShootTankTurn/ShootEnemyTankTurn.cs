using AudioScene;
using Input;
using Pools;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootEnemyTankTurn : Shoot
    {
        [SerializeField] private Transform poolBullTransform;
        [SerializeField] private Transform poolBullSleeveTransform;
        [SerializeField] private ParticleSystem particle;

        private IEnemyTurnPoolExecutor enemyTurnPool;
        private IEnemyTurnSleevePoolExecutor enemyTurnSleevePool;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IEnemyTurnPoolExecutor _enemyTurnPool, IEnemyTurnSleevePoolExecutor _enemyTurnSleevePool)
        {
            audioShoot = _audioShoot;
            enemyTurnPool = _enemyTurnPool;
            enemyTurnSleevePool = _enemyTurnSleevePool;
        }
        protected override void ShootBullet()
        {
            particle.Play();
            audioShoot.OnShootAudio(thisHash, Mode.Turn);
            currentCountClip--;
            enemyTurnPool.GetObject(gameObject.transform.localScale.x, poolBullTransform);
        }
        protected override void ShootBulletSleeve()
        {
            enemyTurnSleevePool.GetObject(gameObject.transform.localScale.x, poolBullSleeveTransform);
        }

    }
}