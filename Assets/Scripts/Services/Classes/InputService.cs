using UnityEngine;
using UnityEngine.InputSystem;

namespace Asteroids.Services
{
    public class InputService : IInputService
    {
        private readonly PlayerInput _playerInput = default;
        public Vector2 Axis { get; set; } = default;
        public bool IsShot { get; set; } = default;

        public InputService()
        {
            _playerInput = new PlayerInput();
        }
        public void Enable()
        {
            _playerInput.Enable();

            _playerInput.Player.Axis.performed += OnAxisInput;
            _playerInput.Player.Axis.canceled += OnAxisInput;
            _playerInput.Player.Shot.started += OnShotStart;
            _playerInput.Player.Shot.canceled += OnShotEnd;
        }
        public void Disable()
        {
            _playerInput.Disable();

            _playerInput.Player.Axis.performed -= OnAxisInput;
            _playerInput.Player.Axis.canceled -= OnAxisInput;
            _playerInput.Player.Shot.started -= OnShotStart;
            _playerInput.Player.Shot.canceled -= OnShotEnd;
        }

        private void OnShotStart(InputAction.CallbackContext param)
        {
            IsShot = true;
        }

        private void OnShotEnd(InputAction.CallbackContext param)
        {
            IsShot = false;
        }

        private void OnAxisInput(InputAction.CallbackContext param)
        {
            Axis = param.ReadValue<Vector2>();
        }
    }
}