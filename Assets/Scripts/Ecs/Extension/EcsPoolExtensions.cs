using Leopotam.EcsLite;

namespace Asteroids.ECS.Ecs.Extension
{
    public static class EcsPoolExtensions
    {
        public static ref T Replace<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity))
                pool.Del(entity);

            return ref pool.Add(entity);
        }

        public static ref T GetOrAdd<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (!pool.Has(entity))
                return ref pool.Add(entity);

            return ref pool.Get(entity);
        }
    }
}