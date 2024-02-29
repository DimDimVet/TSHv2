using Registrator;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Effect
{
    public class AnimControllerMoveEnemyTank : MonoBehaviour
    {
        [SerializeField] private AnimControllerMoveEnemyTankSettings animSettings;
        private Animator animator;
        private float currentVelocity;
        private float speedAnim;
        private string tankEnemyTrackRight, tankEnemyTrackForward, tankEnemyTrackLeft, tankEnemyTrackBack;
        private Construction thisObject;
        private int thisHash;

        private bool isStopClass = false, isRun = false;

        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList)
        {
            dataList = _dataList;
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            thisHash = this.gameObject.GetHashCode();
            thisObject = dataList.GetObjectHash(thisHash);

            if (!isRun)
            {
                if (thisObject.NavMeshAgent is NavMeshAgent)
                {
                    animator = gameObject.GetComponent<Animator>();
                    speedAnim = animSettings.SpeedAnim;
                    tankEnemyTrackRight = animSettings.TankEnemyTrackRight;
                    tankEnemyTrackLeft = animSettings.TankEnemyTrackLeft;
                    tankEnemyTrackForward = animSettings.TankEnemyTrackForward;
                    tankEnemyTrackBack = animSettings.TankEnemyTrackBack;

                    isRun = true;
                }
                else { isRun = false; }
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
            currentVelocity = Mathf.Abs(thisObject.NavMeshAgent.velocity.magnitude);
            if (currentVelocity > 0.1f) { animator.SetFloat(tankEnemyTrackForward, 1); }
            else { animator.SetFloat(tankEnemyTrackForward, 0); }
        }
    }
}

