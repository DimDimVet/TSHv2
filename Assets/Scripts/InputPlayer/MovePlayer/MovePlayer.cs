using Healt;
using Registrator;
using UnityEngine;
using Zenject;

namespace Input
{
    public class MovePlayer : MonoBehaviour
    {
        [SerializeField] private MoveSettings settings;
        private Vector3 angleVelocity;
        private float speedForward, speedBack;
        private Rigidbody rigidbodyGameObject;
        private Quaternion deltaRotation;
        private bool isStopClass = false, isRun = false;
        private int thisHash;

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
            if (thisHash == getHash) { isStopClass = isDead; }
        }

        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                inputs.Enable();

                angleVelocity.y = settings.SpeedTurn;
                speedForward = settings.SpeedForward;
                speedBack = settings.SpeedBack;
                thisHash=gameObject.GetHashCode();
                rigidbodyGameObject = gameObject.GetComponent<Rigidbody>();

                if (!(rigidbodyGameObject is Rigidbody))
                {
                    rigidbodyGameObject = gameObject.AddComponent<Rigidbody>();
                    isRun = false;
                }
                else{isRun = true;}
            }
        }

        void FixedUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            Move();
        }
        private void Move()
        {
            if (inputs.Updata().Move.y > 0)
            {
                rigidbodyGameObject.velocity = transform.forward * speedForward;
            }
            if (inputs.Updata().Move.y < 0)
            {
                rigidbodyGameObject.velocity = -transform.forward * speedBack;
            }

            if (inputs.Updata().Move.x > 0)
            {
                deltaRotation = Quaternion.Euler(angleVelocity * Time.fixedDeltaTime);
                rigidbodyGameObject.MoveRotation(rigidbodyGameObject.rotation * deltaRotation);
            }
            if (inputs.Updata().Move.x < 0)
            {
                deltaRotation = Quaternion.Euler(-angleVelocity * Time.fixedDeltaTime);
                rigidbodyGameObject.MoveRotation(rigidbodyGameObject.rotation * deltaRotation);
            }
        }
    }
}

