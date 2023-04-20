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

        public static ref T AddUnique<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (!pool.Has(entity))
                return ref pool.Add(entity);

            return ref pool.Get(entity);
        }


        public static ref T AddUnique<T>(this EcsPool<T> pool, int entity, out bool isAdd) where T : struct
        {
            if (!pool.Has(entity))
            {
                isAdd = true;

                return ref pool.Add(entity);
            }

            isAdd = false;
            
            return ref pool.Get(entity);
        }

        public static void Delete<T>(this EcsPool<T> pool, int entity) where T : struct
        {
            if (pool.Has(entity))
                pool.Del(entity);
        }
    }
}