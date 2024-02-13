using Input;
using UnityEngine;

namespace CameraMain
{
    public class CameraPointObject : MonoBehaviour
    {
        [SerializeField] private CameraSettings settings;
        public float SpeedMove { get { return speedMove; } set { speedMove = value; } }
        public GameObject CameraTarget { get { return cameraTarget; } set { cameraTarget = value; } }
        public Mode Mode { get { return mode; } set { mode = value; } }
        private Vector3 setVector;
        private float speedMove;
        private Mode mode;
        private GameObject cameraTarget;
        private void Start()
        {
            SetClass();
        }
        private void SetClass()
        {
            setVector = settings.GetAxes();
            speedMove = settings.SpeedMove;
            cameraTarget = new GameObject("cameraTarget");
            cameraTarget.transform.parent = gameObject.transform;
            cameraTarget.transform.position = setVector;
        }

    }
}

