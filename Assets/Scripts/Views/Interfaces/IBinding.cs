using Leopotam.EcsLite;

namespace Asteroids.Views
{
    public interface IBinding<T>
    {
        void Bind(EcsWorld ecsWorld, T param);
    }
}