using UnityEngine;
using Zenject;

namespace EnemyLogic
{
    public class ScanEnemy : MonoBehaviour
    {
        [SerializeField] private ScanEnemySettings settings;
        private float diametrCollider;
        private int thisHash;
        private Collider[] hitColl;

        private bool isStopClass = false, isRun = false;

        private IScanEnemyExecutor scanEnemy;
        [Inject]
        public void Init(IScanEnemyExecutor _scanEnemy)
        {
            scanEnemy = _scanEnemy;
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
                diametrCollider = settings.DiametrCollider;
                isRun = true;
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
            DetectObject();
        }
        private void DetectObject()
        {
            hitColl = Physics.OverlapSphere(this.gameObject.transform.position, diametrCollider);
            scanEnemy.FindPlayer(hitColl, thisHash);

        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(this.gameObject.transform.position, diametrCollider);
        }
    }
}

