using Input;
using Unity.Mathematics;
using UnityEngine;
using Zenject;

namespace Effect
{
    public class ParticleControllerMovePlayer : MonoBehaviour
    {
        [SerializeField] private ParticleSystem partDinamic, partIdle;
        private float refDistance = 0.01f;
        private float2 distans;
        private bool isPartIdle = true;

        private bool isStopClass = false, isRun = false;

        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs)
        {
            inputs = _inputs;
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
            if (!isRun)
            {
                if (partDinamic != null & partIdle != null)
                {
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
            distans.x = Mathf.Abs(inputs.Updata().Move.x);
            distans.y = Mathf.Abs(inputs.Updata().Move.y);
            if (distans.x >= refDistance || distans.y >= refDistance)
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

