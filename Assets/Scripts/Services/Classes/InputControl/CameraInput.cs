using InputControl.Template;

namespace InputControl
{
    public class CameraInput
    {
        public InputAxisModel Zoom { get; set; }
        public InputAxisModel MouseDeltaAxis { get; set; }
        public InputHorizontalModel MouseDeltaHorizontal { get; set; }
        public InputAxisModel MousePositionAxis { get; set; }
        public InputHorizontalModel MousePositionHorizontal { get; set; }
        public InputKeyAndReleaseModel MoveOnStartPositionButton { get; set; }
        public InputKeyAndReleaseModel SwitchCameraStateButton { get; set; }
        public InputKeyAndReleaseModel RightMouseButton { get; set; }
    }
}