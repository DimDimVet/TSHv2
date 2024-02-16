using EnemyLogic;
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
        public int CurrentCountClip { get { return currentCountClip; } set { currentCountClip = value; } }
        public int ThisHash { get { return thisHash; } }
        [SerializeField] private ShootSettings settings;
        private float currentTime, defaultTime, currentTimeClip, defaultTimeClip;
        private bool isBullReLoad = false, isClipReLoad = false, isTrigerSleeve = true;
        private ModeShoot modeShoot;
        private bool isAtackEnemy = false;
        private Mode mode;
        private int maxCountClip, currentCountClip;
        private int thisHash;
        private int count = 0;
        private bool isStopClass = false, isRun = false;

        private IScanEnemyExecutor scanEnemy;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, IScanEnemyExecutor _scanEnemy)
        {
            inputs = _inputs;
            scanEnemy = _scanEnemy;
        }
        private void OnEnable()
        {
            scanEnemy.OnFindPlayer += TargetPlayer;
            scanEnemy.OnLossPlayer += LossTarget;
        }
        private void TargetPlayer(Construction player, int recipientHash)
        {
            if (recipientHash == thisHash) { isAtackEnemy = true; }
        }
        private void LossTarget(int recipientHash)
        {
            if (recipientHash == thisHash) { isAtackEnemy = false; }
        }
        void Start()
        {
            SetClass();
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
            if (currentCountClip <= 0 /*&& mode == inputs.Updata().ModeAction*/)
            {
                currentTimeClip -= Time.deltaTime;
                if (currentTimeClip <= 0)
                {
                    currentTimeClip = defaultTimeClip; isClipReLoad = false; currentCountClip = maxCountClip;
                    return true;
                }
                isClipReLoad = true;
                return false;
            }
            return true;
        }
        private bool ReLoadBullet()
        {
            if (isBullReLoad /*&& mode == inputs.Updata().ModeAction*/)
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
        public virtual void ShootBullet()
        {
        }
        public virtual void ShootBulletSleeve()
        {
        }

    }
}

