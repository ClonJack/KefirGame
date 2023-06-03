using UnityEngine;

namespace InputControl
{
    public interface IAxisInputModel : IPressed, IReleased, IHold
    {
        public Vector2 Axis();
    }
}