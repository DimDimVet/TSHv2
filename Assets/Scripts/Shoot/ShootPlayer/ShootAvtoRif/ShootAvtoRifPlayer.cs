using AudioScene;
using Input;
using Pools;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootAvtoRifPlayer : Shoot
    {
        [SerializeField] private Transform rifPoolTransform;
        [SerializeField] private Transform rifSleevePoolTransform;
        [SerializeField] private ParticleSystem particle;

        private IPlayerRifPoolExecutor playerRifPool;
        private IPlayerRifSleevePoolExecutor playerRifSleevePool;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerRifPoolExecutor _playerRifPool, IPlayerRifSleevePoolExecutor _playerRifSleevePool)
        {
            audioShoot = _audioShoot;
            playerRifPool = _playerRifPool;
            playerRifSleevePool= _playerRifSleevePool;
        }
        protected override void ShootBullet()
        {
            particle.Play();
            audioShoot.OnShootAudio(thisHash, Mode.AvtoRif);
            currentCountClip--;
            playerRifPool.GetObject(gameObject.transform.localScale.x, rifPoolTransform);
        }
        protected override void ShootBulletSleeve()
        {
            playerRifSleevePool.GetObject(gameObject.transform.localScale.y, rifSleevePoolTransform);
        }

    }
}


