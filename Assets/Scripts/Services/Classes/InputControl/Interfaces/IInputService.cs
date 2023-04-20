using UnityEngine;

namespace InputControl
{
    public interface IInputService
    {
        public Vector2 Axis { get; set; }
        public bool IsShot { get; set; }
        public bool IsChangeWeapon { get; set; }
    }
}