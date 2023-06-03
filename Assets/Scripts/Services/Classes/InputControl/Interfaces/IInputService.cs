namespace InputControl
{
    public interface IInputService
    {
        public IAxisInputModel AxisInput { get; set; }
        public IValueInputModel YAxis { get; set; }
        public IValueInputModel XAxis { get; set; }
        public IKeyInputModel Shot { get; set; }
        public IKeyInputModel ChangeWeapon { get; set; }
    }
}