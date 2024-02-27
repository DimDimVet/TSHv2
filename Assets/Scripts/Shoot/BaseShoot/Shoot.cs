using EnemyLogic;
using Healts;
using Input;
using Registrator;
using UnityEngine;
using Zenject;

namespace Shoot
{
    public enum ModeShoot
    {
        Player,
        Enemy
    }
    public class Shoot : MonoBehaviour
    {
        [SerializeField] private ShootSettings settings;
        private float currentTime, defaultTime, currentTimeClip, defaultTimeClip;
        private bool isBullReLoad = false,isTrigerSleeve = true;
        protected bool isClipReLoad = false;
        private ModeShoot modeShoot;
        private bool isAtackEnemy = false;
        private Mode mode;
        private int maxCountClip;
        protected int currentCountClip;
        protected int thisHash;
        private int count = 0;
        private bool isStopClass = false, isRun = false;

        private IHealt healtExecutor;
        private IScanEnemyExecutor scanEnemy;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, IScanEnemyExecutor _scanEnemy,IHealt _healtExecutor)
        {
            inputs = _inputs;
            scanEnemy = _scanEnemy;
            healtExecutor= _healtExecutor;
        }
        private void OnEnable()
        {
            scanEnemy.OnFindPlayer += TargetPlayer;
            scanEnemy.OnLossPlayer += LossTarget;
            healtExecutor.OnIsDead += IsDead;
        }
        private void TargetPlayer(Construction player, int recipientHash)
        {
            if (recipientHash == thisHash) { isAtackEnemy = true; }
        }
        private void LossTarget(int recipientHash)
        {
            if (recipientHash == thisHash) { isAtackEnemy = false; }
        }
        private void IsDead(int getHash, bool isDead, Vector3 _directionDamage)
        {
            if (thisHash == getHash) { isStopClass = isDead; }
        }
        void Start()
        {
            SetClass();
            SetUIParametr();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                thisHash = gameObject.GetHashCode();
                if (thisHash != 0) { isRun = true; }
                else { isRun = false; }

                modeShoot = settings.ModeShoot;
                mode = settings.Mode;

                currentTime = settings.CurrentTime;
                defaultTime = currentTime;

                maxCountClip = settings.MaxCountClip;
                currentCountClip = maxCountClip;
                if (maxCountClip == 1)
                {
                    currentTimeClip = defaultTime;
                    defaultTimeClip = currentTimeClip;
                }
                else
                {
                    currentTimeClip = settings.CurrentTimeClip;
                    defaultTimeClip = currentTimeClip;
                }
            }
        }
        protected virtual void SetUIParametr()
        {
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            ShootActiv();
        }
        private void ShootActiv()
        {
            if (ReLoadBullet() & ReLoadClip())
            {
                if (ModeShoot.Player == modeShoot && mode == inputs.Updata().ModeAction)
                {
                    if (inputs.Updata().MouseLeftButton != 0)
                    {
                        count++;
                        ShootBullet();
                        isBullReLoad = true;
                    }
                }
                else
                {
                    if (ModeShoot.Enemy == modeShoot && isAtackEnemy)
                    {
                        ShootBullet();
                        isBullReLoad = true;
                    }
                }
            }
        }
        private bool ReLoadClip()
        {
            if (currentCountClip <= 0 )
            {
                currentTimeClip -= Time.deltaTime;
                if (currentTimeClip <= 0)
                {
                    currentTimeClip = defaultTimeClip; isClipReLoad = false;
                    currentCountClip = maxCountClip;
                    IsClipReLoad(isClipReLoad);
                    return true;
                }
                isClipReLoad = true;
                IsClipReLoad(isClipReLoad);
                return false;
            }
            return true;
        }
        protected virtual void IsClipReLoad(bool isClipReLoad)
        {
        }
        private bool ReLoadBullet()
        {
            if (isBullReLoad )
            {
                currentTime -= Time.deltaTime;
                if (currentTime <= 2 && isTrigerSleeve)
                {
                    ShootBulletSleeve();
                    isTrigerSleeve = false;
                }
                if (currentTime <= 0)
                {
                    currentTime = defaultTime; isBullReLoad = false; isTrigerSleeve = true;
                    return true;
                }
                return false;
            }
            return true;
        }
        protected virtual void ShootBullet()
        {
        }
        protected virtual void ShootBulletSleeve()
        {
        }

    }
}

