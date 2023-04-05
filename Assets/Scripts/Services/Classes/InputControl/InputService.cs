namespace InputControl
{
    public class InputService : IInputService
    {
        private readonly InputGameControl _inputGameControl;

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
        }
    }
}