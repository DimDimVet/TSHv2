using Input;
using Registrator;
using UnityEngine;

namespace CameraMain
{
    public struct CameraPoint: IConstruction
    {
        public int Hash { get; set; }
        public Mode Mode;
        public Transform PointCamera;
        public Transform LookCamera;
        public float SpeedMove;
    }

    public class CameraPointExecutor : ICameraPointExecutor
    {
        private MasivConstruction<CameraPoint> masiv = new MasivConstruction<CameraPoint>();
        private CameraPoint[] cameraPointList;
        public void SetData(CameraPoint cameraPoint)
        {
            cameraPointList = masiv.Creat(cameraPoint, cameraPointList);
        }
        public void ÑhangeData(CameraPoint cameraPoint)
        {
            for (int i = 0; i < cameraPointList.Length; i++)
            {
                if (cameraPointList[i].Hash == cameraPoint.Hash) { cameraPointList[i]= cameraPoint; }
            }
        }
        public CameraPoint GetDataMode(Mode mode)
        {
            for (int i = 0; i < cameraPointList.Length; i++)
            {
                if (cameraPointList[i].Mode == mode) { return cameraPointList[i]; }
            }
            return new CameraPoint();
        }
    }
}

