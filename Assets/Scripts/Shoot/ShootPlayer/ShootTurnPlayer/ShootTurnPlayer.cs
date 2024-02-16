using AudioScene;
using Input;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public class ShootTurnPlayer : Shoot
    {
        [SerializeField] private Transform poolTransform;
        [SerializeField] private ParticleSystem particle;

        private IAudioShootExecutor audioShoot;
        [Inject]
        public void Init(IAudioShootExecutor _audioShoot)
        {
            audioShoot = _audioShoot;
        }
        public override void ShootBullet()
        {
            Debug.Log("PlayerTurnShoot");
            particle.Play();
            audioShoot.OnShootAudio(ThisHash, Mode.Turn);
            //poolBull.GetObject(gameObject.transform.localScale.x, poolTransform);
        }
        public override void ShootBulletSleeve()
        {
            Debug.Log("PlayerSleeveTurnShoot");
        }

    }
}


