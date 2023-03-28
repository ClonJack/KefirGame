using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AbilityData>> _filter = default;

        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<AbilityData> _abilityDataPool = default;
        private readonly EcsPoolInject<ShotData> _shotDataPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var abilityData = ref _abilityDataPool.Value.Get(entity);

                if (!_attackActionPool.Value.Has(entity))
                {
                    abilityData.Timer = abilityData.CoolDown;
                    continue;
                }

                if (abilityData.Timer <= 0)
                {
                    abilityData.Timer = _attackActionPool.Value.Has(entity) ? abilityData.CoolDown : 0;
                    _shotDataPool.Value.Add(entity);
                }

                abilityData.Timer -= Time.deltaTime;
            }
        }
    }
}