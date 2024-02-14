using Registrator;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Effect
{
    public class ParticleControllerMoveEnemyTank : MonoBehaviour
    {
        [SerializeField] private ParticleSystem partDinamic, partIdle;
        private Construction thisObject;
        private int thisHash;
        private float currentVelocity;
        private bool isPartIdle = true;

        private bool isStopClass = false, isRun = false;

        private IListDataExecutor dataList;
        [Inject]
        public void Init(IListDataExecutor _dataList)
        {
            dataList = _dataList;
        }
        private void OnEnable()
        {
            //isDead = false;
            //OnIsDead += StopRun;
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
                    if (partDinamic != null & partIdle != null)
                    {
                        isRun = true;
                    }
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
            if (currentVelocity > 0.1f)
            {
                if (isPartIdle == false)
                {
                    PartIdle(isPartIdle);
                    PartDinamic(isPartIdle);
                    isPartIdle = true;
                }
            }
            else
            {
                if (isPartIdle == true)
                {
                    PartIdle(isPartIdle);
                    PartDinamic(isPartIdle);
                    isPartIdle = false;
                }
            }
        }
        private void PartIdle(bool isActiv)
        {
            if (isActiv) { partIdle.Play(); }
            else { partIdle.Stop(); }
        }
        private void PartDinamic(bool isActiv)
        {
            if (isActiv) { partDinamic.Stop(); }
            else { partDinamic.Play(); }
        }
    }
}

