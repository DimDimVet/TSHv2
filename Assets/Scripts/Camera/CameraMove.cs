using Input;
using UnityEngine;
using Zenject;

namespace CameraMain
{
    public class CameraMove : MonoBehaviour
    {
        private float speedMove;
        private Transform cameraTransf, pointCamera, lookCamera;
        private Quaternion currRot;
        private Vector3 curPos;
        private Mode tempMode;
        private CameraPoint tempPositionCamera;
        private bool isTriger = true;
        private bool isStopClass = false, isRun = false;

        private ICameraPointExecutor points;
        private IInputPlayerExecutor inputs;
        [Inject]
        public void Init(IInputPlayerExecutor _inputs, ICameraPointExecutor _points)
        {
            points = _points;
            inputs = _inputs;
        }

        void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            if (!isRun)
            {
                tempMode = inputs.Updata().ModeAction;
                cameraTransf = this.gameObject.transform;

                tempPositionCamera = points.GetDataMode(tempMode);
                speedMove = tempPositionCamera.SpeedMove;
                pointCamera = tempPositionCamera.PointCamera;
                lookCamera = tempPositionCamera.LookCamera;

                isRun = true;
            }
        }

        void LateUpdate()
        {
            if (isStopClass) { return; }
            if (!isRun) { SetClass(); }
            RunUpdate();
        }
        private void RunUpdate()
        {
            if (inputs.Updata().ModeAction != tempMode && isTriger || pointCamera == null)
            {
                isTriger = false;
                tempMode = inputs.Updata().ModeAction;
                tempPositionCamera = points.GetDataMode(tempMode);
                speedMove = tempPositionCamera.SpeedMove;
                pointCamera = tempPositionCamera.PointCamera;
                lookCamera = tempPositionCamera.LookCamera;
                isTriger = true;
            }
            curPos = pointCamera.position;
            cameraTransf.position = Vector3.Lerp(a: cameraTransf.position,
                                                 b: curPos,
                                                 t: Time.deltaTime * speedMove);
            currRot = Quaternion.LookRotation(pointCamera.position - cameraTransf.position);
            cameraTransf.rotation = Quaternion.Lerp(a: cameraTransf.rotation,
                                                    b: currRot,
                                                    t: Time.deltaTime * speedMove);
            cameraTransf.LookAt(lookCamera.position);

        }
    }
}

