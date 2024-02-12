using Input;
using Registrator;
using UnityEngine;
using UnityEngine.Windows;
using Zenject;

namespace CameraMain
{
    public class CameraMove : MonoBehaviour
    {
        [SerializeField] private CameraSettings settings;
        private Vector3 setVector;
        private GameObject cameraTarget;
        private float speedMove;
        private Construction player;
        private Transform cameraTransf;
        private Quaternion currRot;
        private Vector3 curPos;
        private bool isStopClass = false, isRun = false;

        private IListDataExecutor dataList;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IListDataExecutor _dataList, IInputPlayerExecutor _inputs)
        {
            inputs = _inputs;
            dataList = _dataList;
        }

        void Start()
        {
            SetClass();
        }

        private void SetClass()
        {
            if (!isRun)
            {
                setVector = settings.GetAxes();
                speedMove = settings.SpeedMove;
                cameraTransf = this.gameObject.transform;

                player = dataList.GetPlayer();
                if (player.Hash != 0)
                {
                    cameraTarget = new GameObject("cameraTarget");
                    cameraTarget.transform.parent = player.Transform;
                    cameraTarget.transform.position = setVector;
                    isRun = true;
                }
                else { isRun = false; }
            }
        }

        void LateUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            if (settings.IsUpDate) { isRun = false; settings.IsUpDate = false; }
            RunUpdate();
        }
        private void RunUpdate()
        {

            curPos = cameraTarget.transform.position;
            cameraTransf.position = Vector3.Lerp(a: cameraTransf.position,
                                                 b: curPos,
                                                 t: Time.deltaTime * speedMove);
            currRot = Quaternion.LookRotation(cameraTarget.transform.position - cameraTransf.position);
            cameraTransf.rotation = Quaternion.Lerp(a: cameraTransf.rotation,
                                                    b: currRot,
                                                    t: Time.deltaTime * speedMove);
            cameraTransf.LookAt(player.Transform.position);
        }
    }
}

