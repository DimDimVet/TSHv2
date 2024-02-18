using AudioScene;
using Input;
using Pools;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootTurnPlayer : Shoot
    {
        [SerializeField] private Transform turnPoolTransform;
        [SerializeField] private Transform turnSleevePoolTransform;
        [SerializeField] private ParticleSystem particle;

        private IPlayerTurnPoolExecutor playerTurnPool;
        private IPlayerTurnSleevePoolExecutor playerTurnSleevePool;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerTurnPoolExecutor _playerTurnPool, IPlayerTurnSleevePoolExecutor _playerTurnSleevePool)
        {
            audioShoot = _audioShoot;
            playerTurnPool = _playerTurnPool;
            playerTurnSleevePool= _playerTurnSleevePool;
        }
        protected override void ShootBullet()
        {
            particle.Play();
            audioShoot.OnShootAudio(thisHash, Mode.Turn);
            currentCountClip--;
            playerTurnPool.GetObject(gameObject.transform.localScale.x, turnPoolTransform);
        }
        protected override void ShootBulletSleeve()
        {
            playerTurnSleevePool.GetObject(gameObject.transform.localScale.y, turnSleevePoolTransform);
        }

    }
}


