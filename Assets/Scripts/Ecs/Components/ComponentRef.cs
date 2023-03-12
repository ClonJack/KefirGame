using UnityEngine;

namespace Asteroids.Components
{
    public struct ComponentRef<T> where T : Component
    {
        public T Component;
    }
}