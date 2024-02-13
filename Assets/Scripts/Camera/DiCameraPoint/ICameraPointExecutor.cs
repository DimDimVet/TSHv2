using Input;

namespace CameraMain
{
    public interface ICameraPointExecutor
    {
        public CameraPoint GetDataMode(Mode mode);
        public void SetData(CameraPoint cameraPoint);
        public void СhangeData(CameraPoint cameraPoint);
    }
}