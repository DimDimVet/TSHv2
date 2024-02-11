namespace Input
{
    public interface IInputPlayerExecutor
    {
        void Enable();
        void OnDisable();
        InputData Updata();
    }
}