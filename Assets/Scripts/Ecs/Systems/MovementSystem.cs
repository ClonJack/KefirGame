using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UnitData, MovementData, DirectionData, RigidbodyData>> _filter = default;
        
        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<UnitData> _unitDataPool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<RigidbodyData> _rigiBodyDataPool = default;

        private Vector3 velicoty;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var movementData = ref _movementDataPool.Value.Get(entity);
                ref var unitData = ref _unitDataPool.Value.Get(entity);
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                ref var rigidbodyData = ref _rigiBodyDataPool.Value.Get(entity);

                unitData.Unit.Rotate(directionData.Rotation * (movementData.RotationSpeed * Time.fixedDeltaTime));
                var target = (directionData.Direction * (movementData.MoveSpeed * Time.fixedDeltaTime));
                rigidbodyData.Rigidbody.AddRelativeForce(target, ForceMode2D.Impulse);
            }
        }
    }
}