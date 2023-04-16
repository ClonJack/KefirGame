using UnityEngine;

namespace InputControl
{
    public class IInputService
    {
        public Vector2 Axis { get; set; }
        public bool IsShot { get; set; }
        public bool IsChangeWeapon { get; set; }
    }
}