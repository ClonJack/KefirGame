using InputControl.Template;

namespace InputControl
{
    public class KeyboardInput
    {
        public InputAxisModel Axis { get; set; }
        public InputKeyAndReleaseModel Shot { get; set; }
        
        public  InputKeyAndReleaseModel ChangeWeapon { get; set; }
    }
}