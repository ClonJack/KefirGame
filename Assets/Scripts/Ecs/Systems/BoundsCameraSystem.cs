using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class BoundsCameraSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player, ComponentRef<Rigidbody2D>>> _filter = default;

        private readonly EcsPoolInject<ComponentRef<Rigidbody2D>> _rigidbodyRefPool = default;

        private Vector3 _maxBoundCamera;
        private Vector3 _minBoundCamera;
        public void Init(IEcsSystems systems)
        {
            _maxBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.one);
            _minBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.zero);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var rigidbodyRef = ref _rigidbodyRefPool.Value.Get(entity);
                var currentPosition = new Vector2(rigidbodyRef.Component.transform.position.x,
                    rigidbodyRef.Component.transform.position.y);
                
                if (currentPosition.y > _maxBoundCamera.y)
                    rigidbodyRef.Component.MovePosition(-currentPosition + Vector2.up);
                else if (currentPosition.y < _minBoundCamera.y)
                    rigidbodyRef.Component.MovePosition(-currentPosition + Vector2.down);
                else if (currentPosition.x > _maxBoundCamera.x)
                    rigidbodyRef.Component.MovePosition(-currentPosition + Vector2.right);
                else if (currentPosition.x < _minBoundCamera.x)
                    rigidbodyRef.Component.MovePosition(-currentPosition + Vector2.left);
                
            }
        }
    }
}