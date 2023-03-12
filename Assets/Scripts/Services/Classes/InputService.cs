using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Services
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _playerInput = default;
        public Vector2 Axis { get; set; }
        public InputService()
        {
            _playerInput = new PlayerInput();
        }
        public void Enable()
        {
            _playerInput.Enable();

            _playerInput.Move.Axis.performed += OnAxisInput;
            _playerInput.Move.Axis.canceled += OnAxisInput;
        }
        public void Disable()
        {
            _playerInput.Disable();

            _playerInput.Move.Axis.performed -= OnAxisInput;
            _playerInput.Move.Axis.canceled -= OnAxisInput;
        }

        private void OnAxisInput(InputAction.CallbackContext param)
        {
            Axis = param.ReadValue<Vector2>();
        }
    }
}