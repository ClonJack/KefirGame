using Asteroids.Components;
using Asteroids.Configuration;
using Configuration;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AbilityData>> _filter = default;

        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<ShotData> _shotDataPool = default;
        private readonly EcsPoolInject<TimerData> _timerDataPool = default;
        private readonly EcsPoolInject<IndexAmmoData> _indexAmmoPool = default; 

        private readonly EcsCustomInject<MainConfig> _mainfConfig;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (!_attackActionPool.Value.Has(entity))
                {
                    if (_timerDataPool.Value.Has(entity))
                        _timerDataPool.Value.Del(entity);

                    continue;
                }

                ref var indexAmmo = ref _indexAmmoPool.Value.Get(entity);
                if (!_timerDataPool.Value.Has(entity))
                {
                    _timerDataPool.Value.Add(entity);
                    var coolDown = _mainfConfig.Value.WeaponConfig.WeaponModels[indexAmmo.Index].Ability.CoolDown;
                    _timerDataPool.Value.Get(entity).Time = coolDown;
                }

                ref var timerData = ref _timerDataPool.Value.Get(entity);
                if (timerData.Time <= 0)
                {
                    _shotDataPool.Value.Add(entity);
                    _timerDataPool.Value.Del(entity);
                    return;
                }

                timerData.Time -= Time.deltaTime;
            }
        }
    }
}