using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlanetMoveSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<Planet>> _filter = default;
        
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<MoveAction> _moveActionPool = default;
        public void Init(IEcsSystems systems)
        {
            var maxBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.one);
            var minBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.zero);
            
            foreach (var entity in _filter.Value)
            {
                var rand = new Vector3(Random.Range(minBoundCamera.x, maxBoundCamera.x),
                    Random.Range(minBoundCamera.y, maxBoundCamera.y));
                
                ref var direction = ref _directionDataPool.Value.Get(entity);
                direction.Direction = rand;

                _moveActionPool.Value.Add(entity);
            }
        }
    }
}