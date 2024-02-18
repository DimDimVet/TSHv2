using EnemyLogic;
using Healt;
using Registrator;
using UnityEngine;
using Zenject;

namespace Input
{
    public class MoveAvtoRif : MonoBehaviour
    {
        [SerializeField] private TurnMoveSettings settings;
        private Mode mode = Mode.AvtoRif;
        private float speedTurn;
        private Construction parentObject;
        private Construction cameraMain;
        private Construction tempTarget;
        private Vector3 currentMousePosition;
        private Ray ray;
        private Vector3 targetDirection;
        private Quaternion targetRotation;
        private int tempHash;
        private bool isStopClass = false, isRun = false;

        private IHealt healtExecutor;
        private IListDataExecutor dataList;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, IListDataExecutor _dataList, IHealt _healtExecutor)
        {
            inputs = _inputs;
            dataList = _dataList;
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            healtExecutor.OnIsDead += IsDead;
        }
        private void IsDead(int getHash, bool isDead)
        {
            if (parentObject.Hash == getHash) { isStopClass = isDead; }
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                parentObject = dataList.GetPlayer();
                cameraMain = dataList.GetCamera();
                if (parentObject.Hash != 0 && cameraMain.CameraComponent != null)
                {
                    speedTurn = settings.SpeedTurn;

                    isRun = true;
                }
                else { isRun = false; }
            }
        }

        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); return; }
            RunUpdate();
        }
        private void RunUpdate()
        {
            TurnMove();
        }
        private void TurnMove()
        {
            if (inputs.Updata().ModeAction == mode)
            {
                currentMousePosition = (Vector2)inputs.Updata().MousePosition;
                ray = cameraMain.CameraComponent.ScreenPointToRay(currentMousePosition);//луч...до мышки
                if (Physics.Raycast(ray, out RaycastHit hitInfo))
                {
                    if (TargetObjectEnemy(hitInfo))
                    {
                        //SelectCursor(true);
                        targetDirection = hitInfo.point - gameObject.transform.position;
                        targetRotation = Quaternion.LookRotation(targetDirection);
                        Debug.DrawRay(gameObject.transform.position, targetDirection, Color.blue);
                        gameObject.transform.rotation =
                            Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * speedTurn);
                    }
                    else
                    {
                        //SelectCursor(false);
                        targetDirection = hitInfo.point - gameObject.transform.position;
                        targetRotation = Quaternion.LookRotation(targetDirection);
                        targetRotation.x = 0;
                        targetRotation.z = 0;
                        Debug.DrawRay(gameObject.transform.position, targetDirection, Color.blue);
                        gameObject.transform.rotation =
                           Quaternion.Lerp(gameObject.transform.rotation, targetRotation, Time.deltaTime * speedTurn);
                    }

                }
            }
        }
        private bool TargetObjectEnemy(RaycastHit hitInfo)
        {
            if (hitInfo.collider != null)
            {
                tempHash = hitInfo.collider.gameObject.GetHashCode();
                tempTarget = dataList.GetObjectHash(tempHash);
                if (tempTarget.TypeObject == TypeObject.Enemy && tempTarget.IsDead == false)
                {
                    return true;
                }
            }
            return false;
        }
    }
}


