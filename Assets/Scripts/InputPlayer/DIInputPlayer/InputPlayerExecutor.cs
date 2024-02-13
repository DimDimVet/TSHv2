using UnityEngine;
using UnityEngine.InputSystem;

namespace Input
{
    public enum Mode
    {
        Turn,
        AvtoRif
    }
    public class InputPlayerExecutor : IInputPlayerExecutor
    {
        private Mode[] modes = { Mode.Turn, Mode.AvtoRif };
        private int countMode = 0;
        private bool isTrigerClick = true;
        private InputData inputData;
        private InputActions inputActions;
        public void Enable()
        {
            inputData = new InputData();
            inputData.Modes = modes;
            inputActions = new InputActions();
            if (inputActions != null)
            {
                //����� Key
                {
                    inputActions.KeyMap.WASD.started += contex => inputData.Move = contex.ReadValue<Vector2>();
                    inputActions.KeyMap.WASD.performed += contex => inputData.Move = contex.ReadValue<Vector2>();
                    inputActions.KeyMap.WASD.canceled += contex => inputData.Move = contex.ReadValue<Vector2>();

                    inputActions.KeyMap.Look.started += contex => { inputData.Mouse = contex.ReadValue<Vector2>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.Look.performed += contex => { inputData.Mouse = contex.ReadValue<Vector2>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.Look.canceled += contex => { inputData.Mouse = contex.ReadValue<Vector2>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };

                    inputActions.KeyMap.MouseLeftButton.started += context => { inputData.MouseLeftButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.MouseLeftButton.performed += context => { inputData.MouseLeftButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.MouseLeftButton.canceled += context => { inputData.MouseLeftButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };

                    inputActions.KeyMap.MouseMiddleButton.started += context => { inputData.MouseMiddleButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.MouseMiddleButton.performed += context => { inputData.MouseMiddleButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.MouseMiddleButton.canceled += context => { inputData.MouseMiddleButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };

                    inputActions.KeyMap.MouseRightButton.started += context => { inputData.MouseRightButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.MouseRightButton.performed += context => { inputData.MouseRightButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };
                    inputActions.KeyMap.MouseRightButton.canceled += context => { inputData.MouseRightButton = context.ReadValue<float>(); inputData.MousePosition = Mouse.current.position.ReadValue(); };

                    inputActions.KeyMap.Shoot.started += context => { inputData.Shoot = context.ReadValue<float>(); };
                    inputActions.KeyMap.Shoot.performed += context => { inputData.Shoot = context.ReadValue<float>(); };
                    inputActions.KeyMap.Shoot.canceled += context => { inputData.Shoot = context.ReadValue<float>(); };

                    inputActions.KeyMap.Mode.started += context => { inputData.Mode = context.ReadValue<float>(); SelectMoveMode(); };
                    inputActions.KeyMap.Mode.performed += context => { inputData.Mode = context.ReadValue<float>(); };
                    inputActions.KeyMap.Mode.canceled += context => { inputData.Mode = context.ReadValue<float>(); };
                }
                //����� UI
                {
                    inputActions.UIMap.WASDUI.started += contex => inputData.Move = contex.ReadValue<Vector2>();
                    inputActions.UIMap.WASDUI.performed += contex => inputData.Move = contex.ReadValue<Vector2>();
                    inputActions.UIMap.WASDUI.canceled += contex => inputData.Move = contex.ReadValue<Vector2>();
                }

                inputActions.Enable();
            }
        }
        public void OnDisable()
        {
            inputActions.Disable();
        }
        private void SelectMoveMode()
        {
            if (inputData.Mode != 0)
            {
                if (isTrigerClick)
                {
                    isTrigerClick = false;
                    countMode++;
                    if (countMode >= modes.Length) { countMode = 0; }

                    for (int i = 0; i < modes.Length; i++)
                    {
                        if ((int)modes[i] == countMode)
                        {
                            inputData.ModeAction = (Mode)countMode;
                        }
                    }
                    isTrigerClick = true;
                }
            }
        }
        public InputData Updata()
        {
            return inputData;
        }
    }
}

