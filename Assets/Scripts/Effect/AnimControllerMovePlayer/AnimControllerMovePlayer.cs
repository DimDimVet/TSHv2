using Input;
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

        private bool isStopClass = false, isRun = false;

        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs)
        {
            inputs = _inputs;
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

