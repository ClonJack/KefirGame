using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class BoundsCameraSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player, ComponentRef<Transform>>> _filter = default;

        private readonly EcsPoolInject<ComponentRef<Transform>> _transformRefPool = default;
        private readonly EcsPoolInject<Player> _playerDataPool = default;

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
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                var currentPosition = new Vector2(transformRef.Component.transform.position.x,
                    transformRef.Component.transform.position.y);
                
                if (currentPosition.y > _maxBoundCamera.y)
                {
                    currentPosition = -currentPosition + Vector2.up;
                }
                
                else if (currentPosition.y < _minBoundCamera.y)
                {
                    currentPosition = -currentPosition + Vector2.down;
                }
                else if (currentPosition.x > _maxBoundCamera.x)
                {
                    currentPosition = -currentPosition + Vector2.right;
                }
                
                else if (currentPosition.x < _minBoundCamera.x)
                {
                    currentPosition = -currentPosition + Vector2.left;
                }

                transformRef.Component.position = currentPosition;
            }
        }
    }
}