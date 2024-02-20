using Healt;
using Registrator;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI
{
    public class UIHealtBar : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private GameObject trackingObject;
        private Camera currentCamera;
        private Construction cameraObject;
        private int thisHash;
        private Canvas canvas;
        private bool isStopClass = false, isRun = false;

        private IListDataExecutor dataList;
        private IHealt healtExecutor;
        [Inject]
        public void Init(IHealt _healtExecutor, IListDataExecutor _dataList)
        {
            dataList = _dataList;
            healtExecutor = _healtExecutor;
        }
        private void OnEnable()
        {
            thisHash = trackingObject.GetHashCode();
            healtExecutor.OnStatisticHealt += ThisUIDamage;
        }
        private void ThisUIDamage(int getHash, int healt, int maxHealt)
        {
            if (thisHash == getHash) { SetUIDamage(healt, maxHealt); }
        }
        private void SetUIDamage(int healt, int maxHealt)
        {
            slider.maxValue = maxHealt;
            slider.value = healt;
        }
        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                if (slider != null & trackingObject != null)
                {
                    cameraObject = dataList.GetCamera();
                    currentCamera = cameraObject.CameraComponent;
                    if (currentCamera == null) { isRun = false; return; }
                    canvas = GetComponent<Canvas>();
                    canvas.planeDistance = 1;
                    canvas.worldCamera = currentCamera;

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
        }

        private void LateUpdate()
        {
            gameObject.transform.LookAt(currentCamera.transform);
        }
    }
}

