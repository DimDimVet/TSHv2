using Registrator;
using UnityEngine;
using Zenject;

namespace Bulls
{
    public enum TypeBullet
    {
        Sleeve,
        PlayerBull,
        PlayerRif,
        EnemyBull,
        EnemyRif
    }
    public class Bull : MonoBehaviour
    {
        [SerializeField] private BulletSettings settings;
        public bool IsForwardPlus { get { return isForwardPlus; } set { isForwardPlus = value; } }
        private Rigidbody rbThisObject;
        private TypeBullet typeBullet;
        private float speedBullet;
        private float killTime, defaultTime;
        private int damage;
        private bool isBullKill = true, isShootTriger = true;
        private bool isForwardPlus = true;
        private RaycastHit hit;
        private float diametrColl;
        private Construction[] data;
        private int tempHash;
        private int thisHash;
        private bool isRun = false, isStopRun = false;
        //
        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList)
        {
            dataList = _dataList;
        }
        //


        //private IHealt healtExecutor;
        //[Inject]
        //public void Init(IHealt h)
        //{
        //    healtExecutor = h;
        //}

        void Start()
        {
            SetSettings();
        }
        private void SetSettings()
        {
            thisHash = gameObject.GetHashCode();
            typeBullet = settings.TypeBullet;
            speedBullet = settings.SpeedBullet;
            killTime = settings.KillTime;
            defaultTime = settings.KillTime;
            damage = settings.Damage;
            diametrColl = settings.DiametrColl;
        }
        private void GetRun()
        {
            if (!isRun)
            {
                rbThisObject = GetComponent<Rigidbody>();
                if (!(rbThisObject is Rigidbody)) { gameObject.AddComponent<Rigidbody>(); }

                data = dataList.GetData();
                if (data != null) { isRun = true; return; }
                isRun = false;
            }
        }
        private void FixedUpdate()
        {
            if (isStopRun) { return; }
            if (!isRun) { GetRun(); }
            MoveBull();
        }
        private void MoveBull()
        {
            if (typeBullet != TypeBullet.Sleeve)
            {
                MoveForward();
                isBullKill = true;
                if (CollisionObject())
                { ReternBullet(); }
                if (KillTimeBullet())
                { ReternBullet(); }
            }
            else if (typeBullet == TypeBullet.Sleeve)
            {
                if (isShootTriger) { rbThisObject.AddForce(Vector3.up * speedBullet, ForceMode.Impulse); }

                isShootTriger = false;
                isBullKill = true;
                if (KillTimeBullet())
                { isShootTriger = true; ShootSleeve(); }
            }
        }
        private void MoveForward()
        {
            if (isForwardPlus) { rbThisObject.velocity = transform.right * speedBullet; }
            else { rbThisObject.velocity = -transform.right * speedBullet; }
        }
        private bool KillTimeBullet()
        {
            if (isBullKill)
            {
                killTime -= Time.deltaTime;
                if (killTime <= 0)
                {
                    killTime = defaultTime; isBullKill = false; return true;
                }
                return false;
            }
            return false;
        }
        private bool CollisionObject()
        {
            Physics.SphereCast(gameObject.transform.position, diametrColl, Vector3.zero, out hit);
            if (hit.collider != null)
            {
                tempHash = hit.collider.gameObject.GetHashCode();
                if (tempHash == thisHash) { return false; }
                if (tempHash != 0) { /*healtExecutor.SetDamage(tempHash, damage);*/Debug.Log($"{tempHash}->{damage}"); return true; }
            }
            return false;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(gameObject.transform.position, diametrColl);
        }
        public virtual void ReternBullet()
        {
        }
        public virtual void ShootSleeve()
        {
        }
    }
}

