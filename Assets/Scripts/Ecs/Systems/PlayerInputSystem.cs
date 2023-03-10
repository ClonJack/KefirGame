using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECS.Systems
{
    public class PlayerInputSystem : IEcsInitSystem
    {
        private readonly EcsFilterInject<Inc<Player, DirectionData>> _filter = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsCustomInject<PlayerInput> _playerInput = default;

        public void Init(IEcsSystems systems)
        {
            _playerInput.Value.Move.Axis.performed += OnAxis;
            _playerInput.Value.Move.Axis.canceled += OnAxis;
        }
        private void OnAxis(InputAction.CallbackContext param)
        {
            var axis = param.ReadValue<Vector2>();
            foreach (var entity in _filter.Value)
            {
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                directionData.Direction = directionData.Forward * -axis.y;
                directionData.Rotation = Vector3.forward * -axis.x;
            }
        }
    }
}