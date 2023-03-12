using Asteroids.Components;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player, DirectionData>> _filter = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsCustomInject<IInputService> _inputServicePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                directionData.Direction = directionData.Forward * -_inputServicePool.Value.Axis.y;
                directionData.Rotation = Vector3.forward * -_inputServicePool.Value.Axis.x;
            }
        }
    }
}