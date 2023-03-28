﻿using UnityEngine.InputSystem;

namespace InputControl.Template
{
    public class InputKeyAndReleaseModel
    {
        private readonly InputAction _inputAction;
        public bool IsReleased => _inputAction.WasReleasedThisFrame();
        public bool IsPressed => _inputAction.WasPressedThisFrame();
        public bool IsHold => _inputAction.IsPressed();
        public InputKeyAndReleaseModel(InputAction inputAction)
        {
            _inputAction = inputAction;
        }
    }
}