using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControl.Template
{
    public class AxisInputModel : IAxisInputModel
    {
        private readonly InputAction _inputAction;

        public AxisInputModel(InputAction inputAction)
        {
            _inputAction = inputAction;
        }

        public bool IsPressed() => _inputAction.WasPressedThisFrame();
        public bool IsReleased() => _inputAction.WasReleasedThisFrame();
        public bool IsHold() => _inputAction.IsPressed();
        public Vector2 Axis() => _inputAction.ReadValue<Vector2>();
    }
}