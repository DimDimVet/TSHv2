using AudioScene;
using Input;
using Pools;
using UI;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootAvtoRifPlayer : Shoot
    {
        [SerializeField] private Transform rifPoolTransform;
        [SerializeField] private Transform rifSleevePoolTransform;
        [SerializeField] private ParticleSystem particle;
        private Mode thisMode = Mode.AvtoRif;

        private IUIPanelsExecutor panels;
        private IPlayerRifPoolExecutor playerRifPool;
        private IPlayerRifSleevePoolExecutor playerRifSleevePool;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerRifPoolExecutor _playerRifPool,
                         IPlayerRifSleevePoolExecutor _playerRifSleevePool, IUIPanelsExecutor _panels)
        {
            audioShoot = _audioShoot;
            playerRifPool = _playerRifPool;
            playerRifSleevePool= _playerRifSleevePool;
            panels = _panels;
        }
        protected override void SetUIParametr()
        {
            panels.ChargingSetParametr(thisMode, currentCountClip);
            panels.OnCurrentMode += ChargingUpdate;
        }
        private void ChargingUpdate(Mode mode)
        {
            if (mode == thisMode) { panels.ChargingUpdate(mode, isClipReLoad, currentCountClip);}
        }
        protected override void IsClipReLoad(bool isClipReLoad)
        {
            panels.ChargingUpdate(thisMode, isClipReLoad, currentCountClip);
        }
        protected override void ShootBullet()
        {
            particle.Play();
            audioShoot.OnShootAudio(thisHash, thisMode);
            currentCountClip--;
            playerRifPool.GetObject(gameObject.transform.localScale.x, rifPoolTransform);
            ChargingUpdate(thisMode);
        }
        protected override void ShootBulletSleeve()
        {
            playerRifSleevePool.GetObject(gameObject.transform.localScale.y, rifSleevePoolTransform);
        }
    }
}


