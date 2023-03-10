using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class MovementSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<TransformRef, MovementData, DirectionData, RigidbodyRef>> _filter = default;

        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<TransformRef> _unitDataPool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<RigidbodyRef> _rigiBodyDataPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var movementData = ref _movementDataPool.Value.Get(entity);
                ref var unitData = ref _unitDataPool.Value.Get(entity);
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                ref var rigidbodyData = ref _rigiBodyDataPool.Value.Get(entity);

                var targetRotate = directionData.Rotation * (movementData.RotationSpeed * Time.fixedDeltaTime);
                unitData.Unit.Rotate(targetRotate);
                
                var targetMove = (directionData.Direction * (movementData.MoveSpeed * Time.fixedDeltaTime));
                rigidbodyData.Rigidbody.AddRelativeForce(targetMove, ForceMode2D.Impulse);
            }
        }
    }
}