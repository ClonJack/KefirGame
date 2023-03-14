using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackAction, CoolDownData>> _filter = default;
        private readonly EcsPoolInject<CoolDownData> _coolDownDataPool = default;
        private readonly EcsPoolInject<ShotData> _shotData = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var coolDownData = ref _coolDownDataPool.Value.Get(entity);
                coolDownData.CoolDown -= Time.deltaTime;
                if (coolDownData.CoolDown <= 0)
                {
                    coolDownData.CoolDown = coolDownData.MaxCoolDown;
                    _shotData.Value.Add(entity);
                }
            }
        }
        
        
    }
}