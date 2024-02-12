using Input;
using Registrator;
using UnityEngine;
using Zenject;

namespace EnemyLogic
{
    public class MoveTurnEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject parentTurnObject;
        [SerializeField] private TurnMoveSettings settings;
        private Quaternion defaultTransform;
        private Transform target;
        private Vector3 targetDirection;
        private Quaternion targetRotation;
        private float speedTurn, maxOffSetX;
        private int thisHash;
        private bool isStopClass = false, isRun = false;

        private IScanEnemyExecutor scanEnemy;
        [Inject]
        public void Init(IScanEnemyExecutor _scanEnemy)
        {
            scanEnemy = _scanEnemy;
        }
        private void OnEnable()
        {
            scanEnemy.OnFindPlayer += TargetPlayer;
            scanEnemy.OnLossPlayer += LossTarget;
        }
        private void TargetPlayer(Construction player, int recipientHash)
        {
            if (recipientHash == thisHash) { target = player.Transform; }
        }
        private void LossTarget(int recipientHash)
        {
            if (recipientHash == thisHash) { target = null; }
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            thisHash = this.gameObject.GetHashCode();
            if (!isRun)
            {
                defaultTransform = parentTurnObject.transform.rotation;

                speedTurn = settings.SpeedTurn;
                maxOffSetX = settings.MaxOffSetX;

                isRun = true;
            }
            else { isRun = false; }
        }

        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            StepTarget();
        }
        private void DefaultPosition()
        {
            parentTurnObject.transform.rotation =
                Quaternion.Lerp(parentTurnObject.transform.rotation, defaultTransform, Time.deltaTime * speedTurn);
        }
        private void StepTarget()
        {
            if (target == null) { DefaultPosition(); return; }
            targetDirection = target.position - parentTurnObject.transform.position;
            targetRotation = Quaternion.LookRotation(targetDirection);
            if (targetRotation.x > maxOffSetX) { targetRotation.x = maxOffSetX; }
            targetRotation.z = 0;
            parentTurnObject.transform.rotation =
                Quaternion.Lerp(parentTurnObject.transform.rotation, targetRotation, Time.deltaTime * speedTurn);
        }
    }
}

