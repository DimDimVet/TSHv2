using Healts;
using UnityEngine;
using Zenject;

namespace Destroy
{
    public class DestroyEnemy : MonoBehaviour
    {

        [SerializeField] private Rigidbody[] rigidbodies;
        [SerializeField, Range(1, 100)] private float forceImpulse = 50f;
        private Vector3 directionDamage;
        public bool isStart = false;
        private int thisHash;
        private bool isStopClass = false, isRun = false;

        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor)
        {
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            healtExecutor.OnIsDead += IsDead;
        }
        private void IsDead(int getHash, bool isDead, Vector3 _directionDamage)
        {
            if (thisHash == getHash) { isStart = true; directionDamage = _directionDamage; }
        }
        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                if (rigidbodies != null)
                {
                    for (int i = 0; i < rigidbodies.Length; i++)
                    {
                        rigidbodies[i].isKinematic = true;
                    }

                    thisHash = gameObject.GetInstanceID();
                    isRun = true;
                }
                else { isRun = false; }
            }
        }

        void Update()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
        }

        private void FixedUpdate()
        {
            RunUpdate();
        }
        private void RunUpdate()
        {
            if (isStart)
            {
                for (int i = 0; i < rigidbodies.Length; i++)
                {
                    rigidbodies[i].isKinematic = false;
                    rigidbodies[i].AddForce(Vector3.up * forceImpulse, ForceMode.Impulse);
                    rigidbodies[i].AddForce(directionDamage * forceImpulse / 2, ForceMode.Impulse);
                }
                isStart = !isStart;
            }
        }
    }
}

