﻿using InputControl.Template;

namespace InputControl
{
    public class InputGameControl 
    {
        private GameInput _gameInput;

        private CameraInput _cameraInput;
        private KeyboardInput _keyboardInput;
        private MouseInput _mouseInput;
        public MouseInput MouseInput => _mouseInput;
        public CameraInput CameraInput => _cameraInput;
        public KeyboardInput KeyboardInput => _keyboardInput;
        public void Init()
        {
            _gameInput = new GameInput();
            
            _cameraInput = new CameraInput()
            {
            };

            _keyboardInput = new KeyboardInput()
            {
                Axis = new InputAxisModel(_gameInput.Keyboard.Axis),
                Shot = new InputKeyAndReleaseModel(_gameInput.Keyboard.Shot),
                ChangeWeapon = new InputKeyAndReleaseModel(_gameInput.Keyboard.ChangeWeapon)
            };

            _mouseInput = new MouseInput()
            {
            };
            
            _gameInput.Enable();
        }
        public void Enable()
        {
            _gameInput.Enable();
        }
        public void Disable()
        {
            _gameInput.Disable();
            _gameInput.Dispose();
        }
    }
}