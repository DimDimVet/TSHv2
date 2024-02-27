using Healts;
using Registrator;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace EnemyLogic
{
    public class MoveEnemy : MonoBehaviour
    {
        [SerializeField] private MoveEnemySettings settings;
        private int thisHash;
        private Construction thisObject;
        private Transform currentPosition;
        private Vector3 defaultPositions;
        private float speedMove, speedAngle, acceleration, stopDistance;
        //private int countTarget1 = 0, countTargetDefault1 = 0;
        //private Vector3 currentTarget1;
        private bool isTriger = true;
        private bool isStopClass = false, isRun = false;

        private IHealt healtExecutor;
        private IScanEnemyExecutor scanEnemy;
        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList, IScanEnemyExecutor _scanEnemy, IHealt _healtExecutor)
        {
            dataList = _dataList;
            scanEnemy = _scanEnemy;
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            scanEnemy.OnFindPlayer += TargetPlayer;
            scanEnemy.OnLossPlayer += LossTarget;
            healtExecutor.OnIsDead += IsDead;
        }
        private void TargetPlayer(Construction player, int recipientHash)
        {
            if (recipientHash == thisHash) { currentPosition = player.Transform; }
        }
        private void LossTarget(int recipientHash)
        {
            if (recipientHash == thisHash) { currentPosition = null; }
        }
        private void IsDead(int getHash, bool isDead, Vector3 _directionDamage)
        {
            if (thisHash == getHash)
            {
                isStopClass = isDead;
                thisObject.NavMeshAgent.enabled = false;
            }
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            thisHash = this.gameObject.GetHashCode();
            thisObject = dataList.GetObjectHash(thisHash);
            defaultPositions = gameObject.transform.position;

            if (!isRun)
            {
                if (thisObject.NavMeshAgent is NavMeshAgent)
                {
                    speedMove = settings.SpeedMove;
                    speedAngle = settings.SpeedAngle;
                    acceleration = settings.Acceleration;
                    stopDistance = settings.StopDistance;
                    SetNavComponent();

                    isRun = true;
                }
                else { isRun = false; }
            }
        }
        private void SetNavComponent()
        {
            thisObject.NavMeshAgent.speed = speedMove;
            thisObject.NavMeshAgent.angularSpeed = speedAngle;
            thisObject.NavMeshAgent.acceleration = acceleration;
            thisObject.NavMeshAgent.stoppingDistance = stopDistance;
        }
        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            CycleTarget();
        }
        private void CycleTarget()
        {
            if (isRun)
            {
                if (isTriger)
                {
                    //StepTarget();
                    if (currentPosition != null) { EnemyMove(currentPosition.position); }
                    else { EnemyMove(defaultPositions); }

                    isTriger = false;
                }
                else
                {
                    if (thisObject.NavMeshAgent.velocity.magnitude <= 0.1f) { isTriger = true; }
                }
            }
        }
        private void EnemyMove(Vector3 _currentTarget)
        {
            thisObject.NavMeshAgent.SetDestination(_currentTarget);
        }

    }
}

