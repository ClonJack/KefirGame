using UnityEngine;
using UnityEngine.InputSystem;

namespace InputControl.Template
{
    public class InputAxisModel
    {
        private readonly InputAction _inputAction;
        public Vector2 Value => _inputAction.ReadValue<Vector2>();
        public InputAxisModel(InputAction inputAction)
        {
            _inputAction = inputAction;
        }
    }
}