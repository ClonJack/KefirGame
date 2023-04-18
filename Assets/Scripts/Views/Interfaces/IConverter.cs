using Leopotam.EcsLite;

namespace Asteroids.Views
{
    public interface IConverter
    {
        void Convert(EcsWorld ecsWorld);
    }
}