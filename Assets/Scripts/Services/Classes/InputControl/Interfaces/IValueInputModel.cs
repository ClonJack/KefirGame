namespace InputControl
{
    public interface IValueInputModel : IPressed, IReleased, IHold
    {
        float Value();
    }
}