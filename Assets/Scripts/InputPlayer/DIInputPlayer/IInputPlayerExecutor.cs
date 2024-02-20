using System;

namespace Input
{
    public interface IInputPlayerExecutor
    {
        Action<InputData> OnEventUpdata { get; set; }
        void Enable();
        void OnDisable();
        InputData Updata();
    }
}