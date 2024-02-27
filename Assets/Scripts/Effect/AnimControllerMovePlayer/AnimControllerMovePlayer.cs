using Healts;
using Input;
using Registrator;
using UnityEngine;
using Zenject;

namespace Effect
{
    public class AnimControllerMovePlayer : MonoBehaviour
    {
        [SerializeField] private AnimControllerMovePlayerSettings animSettings;
        private Animator animator;
        private float speedAnim;
        private string tankPlayerTrackRight, tankPlayerTrackForward, tankPlayerTrackLeft, tankPlayerTrackBack;
        private int thisHash;
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
        private void IsDead(int getHash, bool isDead, Vector3 _directionDamage)
        {
            if (thisHash == getHash) { isStopClass = isDead; }
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            speedAnim = animSettings.SpeedAnim;
            tankPlayerTrackRight = animSettings.TankPlayerTrackRight;
            tankPlayerTrackLeft = animSettings.TankPlayerTrackLeft;
            tankPlayerTrackForward = animSettings.TankPlayerTrackForward;
            tankPlayerTrackBack = animSettings.TankPlayerTrackBack;

            if (!isRun)
            {
                animator = gameObject.GetComponent<Animator>();
                thisHash = gameObject.GetHashCode();
                if (animator != null) { isRun = true; }
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
            if (inputs.Updata().Move.y > 0) { animator.SetFloat(tankPlayerTrackForward, speedAnim); }
            else { animator.SetFloat(tankPlayerTrackForward, 0); }

            if (inputs.Updata().Move.y < 0) { animator.SetFloat(tankPlayerTrackBack, speedAnim); }
            else { animator.SetFloat(tankPlayerTrackBack, 0); }

            if (inputs.Updata().Move.x > 0) { animator.SetFloat(tankPlayerTrackRight, speedAnim); }
            else { animator.SetFloat(tankPlayerTrackRight, 0); }

            if (inputs.Updata().Move.x < 0) { animator.SetFloat(tankPlayerTrackLeft, speedAnim); }
            else { animator.SetFloat(tankPlayerTrackLeft, 0); }
        }
    }
}

