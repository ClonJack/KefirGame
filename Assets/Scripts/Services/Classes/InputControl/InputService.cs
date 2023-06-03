using System;
using InputControl.Template;

namespace InputControl
{
    public class InputService : IInputService, IDisposable
    {
        private GameInput _gameInput = null;
        public IAxisInputModel AxisInput { get; set; }
        public IValueInputModel YAxis { get; set; }
        public IValueInputModel XAxis { get; set; }
        public IKeyInputModel Shot { get; set; }
        public IKeyInputModel ChangeWeapon { get; set; }

        public InputService()
        {
            _gameInput = new GameInput();
            
            _gameInput.Enable();

            AxisInput = new AxisInputModel(_gameInput.Keyboard.Axis);

            YAxis = new InputValueModel(_gameInput.Keyboard.YAxis);
            
            XAxis = new InputValueModel(_gameInput.Keyboard.XAxis);

            Shot = new InputKeyModel(_gameInput.Keyboard.Shot);
            
            ChangeWeapon = new InputKeyModel(_gameInput.Keyboard.ChangeWeapon);
        }

        public void Dispose()
        {
            _gameInput?.Dispose();
            _gameInput?.Disable();
        }
    }
}