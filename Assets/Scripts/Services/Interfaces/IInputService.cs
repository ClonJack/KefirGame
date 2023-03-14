using UnityEngine;

namespace Asteroids.Services
{
    public interface IInputService
    {
        public Vector2 Axis { get; set; }
        public bool IsShot { get; set; }
    }
}