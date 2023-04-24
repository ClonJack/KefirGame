using Leopotam.EcsLite;

namespace Asteroids.Views
{
    public interface IConverter
    {
        void Convert(IEcsSystems ecsWorld);
    }
}