using UnityEngine;

namespace InputControl
{
    public class InputService : IInputService
    {
        private readonly InputGameControl _inputGameControl;

        public Vector2 Axis { get; set; }
        public bool IsShot { get; set; }
        public bool IsChangeWeapon { get; set; }

        public InputService(InputGameControl inputGameControl)
        {
            _inputGameControl = inputGameControl;
        }

        public void Update()
        {
            Keyboard();
        }

        private void Keyboard()
        {
            Axis = _inputGameControl.KeyboardInput.Axis.Value;
            IsShot = _inputGameControl.KeyboardInput.Shot.IsHold;
            IsChangeWeapon = _inputGameControl.KeyboardInput.ChangeWeapon.IsPressed;
        }
    }
}