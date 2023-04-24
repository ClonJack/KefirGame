using Asteroids.Components;
using Asteroids.Configuration;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class WeaponReloadSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AbilityData>> _filter = default;

        private readonly EcsPoolInject<ShotData> _shotDataPool = default;
        private readonly EcsPoolInject<TimerData> _timerDataPool = default;
        private readonly EcsPoolInject<IndexAmmoData> _indexAmmoPool = default;
        private readonly EcsPoolInject<ReloadAction> _attackProcessPool = default;

        private readonly EcsCustomInject<MainConfig> _mainfConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (!_attackProcessPool.Value.Has(entity))
                {
                    _timerDataPool.Value.Del(entity);
                    continue;
                }

                ref var indexAmmo = ref _indexAmmoPool.Value.Get(entity);
                
                if (!_timerDataPool.Value.Has(entity))
                {
                   _timerDataPool.Value.Add(entity).Time = _mainfConfig.Value.WeaponsConfig.Models[indexAmmo.Index].Ability.CoolDown;
                }

                ref var timeData = ref _timerDataPool.Value.Get(entity);

                if (timeData.Time <= 0)
                {
                    _shotDataPool.Value.Add(entity);
                    _timerDataPool.Value.Del(entity);
                    return;
                }

                timeData.Time -= Time.deltaTime;
            }
        }
    }
}