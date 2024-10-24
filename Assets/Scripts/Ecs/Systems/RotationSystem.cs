using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class RotationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<RotateAction>> _filter = default;
        
        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<ComponentRef<Rigidbody2D>> _rigiBodyRefPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var movementData = ref _movementDataPool.Value.Get(entity);
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                ref var rigidbodyRef = ref _rigiBodyRefPool.Value.Get(entity);

                var targetRotate = Vector3.forward *
                                   (directionData.Direction.x * (movementData.RotationSpeed * Time.fixedDeltaTime));

                var quaternion = Quaternion.Euler(targetRotate);

                var localRotate = rigidbodyRef.Component.transform.localRotation;

                rigidbodyRef.Component.MoveRotation(localRotate * quaternion);
            }
        }
    }
}