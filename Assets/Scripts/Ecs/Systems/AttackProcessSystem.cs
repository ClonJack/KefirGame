using Asteroids.Components;
using Asteroids.Configuration;
using Asteroids.ECS.Ecs.Extension;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackProcessSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AbilityData>> _filter = default;

        private readonly EcsPoolInject<ShotData> _shotDataPool = default;
        private readonly EcsPoolInject<TimerData> _timerDataPool = default;
        private readonly EcsPoolInject<IndexAmmoData> _indexAmmoPool = default;
        private readonly EcsPoolInject<AttackProcessAction> _attackProcessPool = default;

        private readonly EcsCustomInject<MainConfig> _mainfConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                if (!_attackProcessPool.Value.Has(entity))
                {
                    _timerDataPool.Value.Delete(entity);
                    continue;
                }

                ref var indexAmmo = ref _indexAmmoPool.Value.Get(entity);

                ref var timeData = ref _timerDataPool.Value.AddUnique(entity, out var isAdd);
                
                if (isAdd)
                    timeData.Time = _mainfConfig.Value.WeaponConfig.WeaponModels[indexAmmo.Index].Ability.CoolDown;

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