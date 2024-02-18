using AudioScene;
using Input;
using Pools;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootEnemyAvtoRif : Shoot
    {
        [SerializeField] private Transform poolBullTransform;
        [SerializeField] private Transform poolBullSleeveTransform;
        [SerializeField] private ParticleSystem particle;

        private IEnemyRifPoolExecutor enemyRifPool;
        private IEnemyRifSleevePoolExecutor enemyRifSleevePool;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IEnemyRifPoolExecutor _enemyRifPool, IEnemyRifSleevePoolExecutor _enemyRifSleevePool)
        {
            audioShoot = _audioShoot;
            enemyRifPool = _enemyRifPool;
            enemyRifSleevePool = _enemyRifSleevePool;
        }
        protected override void ShootBullet()
        {
            particle.Play();
            audioShoot.OnShootAudio(thisHash, Mode.Turn);
            currentCountClip--;
            enemyRifPool.GetObject(gameObject.transform.localScale.x, poolBullTransform);
        }
        protected override void ShootBulletSleeve()
        {
            enemyRifSleevePool.GetObject(gameObject.transform.localScale.x, poolBullSleeveTransform);
        }

    }
}