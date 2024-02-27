using Bulls;
using Healt;
using Registrator;
using UnityEngine;
using Zenject;

namespace Loot
{
    public class Loot : MonoBehaviour
    {
        [SerializeField] private LootSettings settings;
        private int plusHealt;

        private Rigidbody rbThisObject;
        private TypeBullet typeBullet;
        private float speedMoveLoot;
        private float killTime, defaultTime;
        private int damage;
        private float percentDamage;
        private float percent, currentDamag;
        private bool isBullKill = true, isShootTriger = true;
        private RaycastHit hit;
        private float diametrColl, maxDistance;
        private Construction[] data;
        private int tempHash;
        private int thisHash;
        private bool isRun = false, isStopRun = false;
        //
        private IHealt healtExecutor;
        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList, IHealt _healtExecutor)
        {
            dataList = _dataList;
            healtExecutor = _healtExecutor;
        }

        void Start()
        {
            SetSettings();
        }
        private void SetSettings()
        {
            thisHash = gameObject.GetHashCode();
            plusHealt = settings.PlusHealt;
            speedMoveLoot = settings.SpeedMoveLoot;
        }
        private void GetRun()
        {
            if (!isRun)
            {
                rbThisObject =gameObject.GetComponent<Rigidbody>();
                if (!(rbThisObject is Rigidbody)) { gameObject.AddComponent<Rigidbody>(); }

                data = dataList.GetData();
                if (data != null)
                { isRun = true; return; }
                else { isRun = false; }
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
            //if (typeBullet != TypeBullet.Sleeve)
            //{
            //    rbThisObject.velocity = transform.forward * speedBullet;
            //    isBullKill = true;
            //    if (CollisionObject())
            //    { ReternBullet(); }
            //    if (KillTimeBullet())
            //    { ReternBullet(); }
            //}
            //else if (typeBullet == TypeBullet.Sleeve)
            //{
            //    if (isShootTriger) { rbThisObject.AddForce(transform.forward * speedBullet, ForceMode.Impulse); }

            //    isShootTriger = false;
            //    isBullKill = true;
            //    if (KillTimeBullet())
            //    { isShootTriger = true; ShootSleeve(); }
            //}
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
            Physics.SphereCast(gameObject.transform.position, diametrColl, gameObject.transform.forward, out hit, maxDistance);

            if (hit.collider != null)
            {
                tempHash = hit.collider.gameObject.GetHashCode();
                if (tempHash == thisHash) { return false; }
                if (tempHash != 0)
                {
                    healtExecutor.SetDamage(tempHash, DamagRandom(), gameObject.transform.forward);
                    return true;
                }
            }
            return false;
        }
        private int DamagRandom()
        {
            percent = Random.Range(1, percentDamage);
            currentDamag = Random.value * damage * percent;
            return (int)currentDamag;
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(gameObject.transform.position, gameObject.transform.position + gameObject.transform.forward);
            Gizmos.DrawWireSphere(gameObject.transform.position + gameObject.transform.forward, diametrColl);
        }
        protected virtual void ReternBullet()
        {
        }
        protected virtual void ShootSleeve()
        {
        }
    }
}

