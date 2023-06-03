using UnityEngine.InputSystem;

namespace InputControl.Template
{
    public class InputKeyModel : IKeyInputModel
    {
        private readonly InputAction _inputAction;
        public InputKeyModel(InputAction inputAction)
        {
            _inputAction = inputAction;
        }
        public bool IsPressed() => _inputAction.WasPressedThisFrame();
        public bool IsReleased() => _inputAction.WasReleasedThisFrame();
        public bool IsHold() => _inputAction.IsPressed();
    }
}