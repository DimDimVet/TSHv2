using CameraMain;
using Input;
using UnityEngine;
using Zenject;

public class SetCameraPoint : MonoBehaviour
{
    [Header("Объект установки камеры")]
    [SerializeField] private Transform pointCamera;
    [Header("Объект установки Look камеры")]
    [SerializeField] private Transform lookCamera;
    [SerializeField, Range(0, 30)] private float speedMove = 1;
    [Header("Режим")]
    [SerializeField] private Mode mode;

    private CameraPoint cameraPoint;
    private float tempSpeed;
    private int thisHash;
    private bool isStopClass = false, isRun = false;

    private ICameraPointExecutor points;
    [Inject]
    public void Init(ICameraPointExecutor _points)
    {
        points = _points;
    }
    void Start()
    {
        SetClass();
    }
    private void SetClass()
    {
        thisHash = gameObject.GetHashCode();
        tempSpeed = speedMove;

        if (!isRun)
        {
            if (pointCamera != null && lookCamera != null)
            {
                cameraPoint = new CameraPoint
                {
                    Hash = thisHash,
                    PointCamera = pointCamera,
                    LookCamera = lookCamera,
                    SpeedMove = tempSpeed,
                    Mode = mode
                };
                points.SetData(cameraPoint);
                isRun = true;
            }

        }
        else { isRun = false; }
    }
    void Update()
    {
        if (isStopClass) { return; }
        if (!isRun) { SetClass(); }
        RunUpdate();
    }
    private void RunUpdate()
    {
        if (tempSpeed != speedMove)
        {
            cameraPoint.PointCamera = pointCamera;
            cameraPoint.SpeedMove = tempSpeed;
            points.СhangeData(cameraPoint);
        }
    }

}
