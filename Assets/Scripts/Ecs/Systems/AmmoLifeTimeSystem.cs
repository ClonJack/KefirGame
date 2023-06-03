using Asteroids.Components;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AmmoLifeTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AmmoLifeTimeData>> _filter = default;
        private readonly EcsPoolInject<AmmoLifeTimeData> _coolDownDataPool = default;
        private readonly EcsCustomInject<PoolServices> _servicesRefPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var coolDownData = ref _coolDownDataPool.Value.Get(entity);

                coolDownData.Timer -= Time.deltaTime;
                if (coolDownData.Timer <= 0)
                {
                    _servicesRefPool.Value.AmmoPool.GetPool().Release(coolDownData.Ammo);
                    
                    _coolDownDataPool.Value.Del(entity);
                }
            }
        }
    }
}