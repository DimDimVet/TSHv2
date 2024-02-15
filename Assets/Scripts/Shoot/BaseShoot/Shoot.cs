using Input;
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
        private int maxCountClip, currentCountClip;
        private int thisHash;
        private bool isRun = false, isStopRun = false;
        private int count = 0;
        //
        //private IHealt healtExecutor;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs)
        {
            inputs = _inputs;
        }
        //public void Init(IInput i, IHealt h)
        //{
        //    inputData = i;
        //    healtExecutor = h;
        //}
        private void OnEnable()
        {
            //healtExecutor.OnIsDead += IsDead;
        }
        void Start()
        {
            Set();
            SetSettings();
        }
        public virtual void Set()
        {
        }
        private void SetSettings()
        {
            thisHash = gameObject.GetHashCode();

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
            modeShoot = settings.ModeShoot;
        }
        private void GetRun()
        {
            if (!isRun)
            {
                isRun = true;
            }
        }
        void Update()
        {
            if (isStopRun) { return; }
            if (!isRun) { GetRun(); }
            ShootActiv();
        }
        private void IsDead(int getHash, bool isDead)
        {
            if (thisHash == getHash) { isStopRun = isDead; }
        }
        private void ShootActiv()
        {
            if (ReLoadBullet() & ReLoadClip())
            {
                if (ModeShoot.Player == modeShoot)
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
                    if (ModeShoot.Enemy == modeShoot)
                    {
                        ShootBullet();
                        isBullReLoad = true;
                    }
                }
            }
        }
        private bool ReLoadClip()
        {
            if (currentCountClip <= 0)
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
            if (isBullReLoad)
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


