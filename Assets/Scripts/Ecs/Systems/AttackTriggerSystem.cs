using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class ShootTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackAction, DirectionData, ComponentRef<Rigidbody2D>>> _filter = default;
        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var attackAction = ref _attackActionPool.Value.Get(entity);
                
                Debug.Log("Work");
            }
        }
    }
}