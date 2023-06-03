using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlanetMoveSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlanetAction>> _filter = default;

        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<MoveAction> _moveActionPool = default;

        private Vector2 _maxBoundCamera = default;
        private Vector2 _minBoundCamera = default;

        public void Init(IEcsSystems systems)
        {
            _maxBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.one);
            _minBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.zero);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var rand = new Vector3(Random.Range(_minBoundCamera.x, _maxBoundCamera.x),
                    Random.Range(_minBoundCamera.y, _maxBoundCamera.y));

                ref var direction = ref _directionDataPool.Value.Get(entity);
                direction.Direction = rand;

                _moveActionPool.Value.Add(entity);
            }
        }
    }
}