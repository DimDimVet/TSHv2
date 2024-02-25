using AudioScene;
using Input;
using Pools;
using UI;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootTurnPlayer : Shoot
    {
        [SerializeField] private Transform turnPoolTransform;
        [SerializeField] private Transform turnSleevePoolTransform;
        [SerializeField] private ParticleSystem particle;
        private Mode thisMode = Mode.Turn;

        private IUIPanelsExecutor panels;
        private IPlayerTurnPoolExecutor playerTurnPool;
        private IPlayerTurnSleevePoolExecutor playerTurnSleevePool;
        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot, IPlayerTurnPoolExecutor _playerTurnPool,
                         IPlayerTurnSleevePoolExecutor _playerTurnSleevePool, IUIPanelsExecutor _panels)
        {
            audioShoot = _audioShoot;
            playerTurnPool = _playerTurnPool;
            playerTurnSleevePool = _playerTurnSleevePool;
            panels = _panels;
        }
        protected override void SetUIParametr()
        {
            panels.ChargingSetParametr(thisMode, currentCountClip);
            panels.OnCurrentMode += ChargingUpdate;
        }

        private void ChargingUpdate(Mode mode)
        {
            if (mode == thisMode) { panels.ChargingUpdate(mode, isClipReLoad, currentCountClip); }
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
            playerTurnPool.GetObject(gameObject.transform.localScale.x, turnPoolTransform);
            ChargingUpdate(thisMode);
        }
        protected override void ShootBulletSleeve()
        {
            playerTurnSleevePool.GetObject(gameObject.transform.localScale.y, turnSleevePoolTransform);
        }
    }
}


