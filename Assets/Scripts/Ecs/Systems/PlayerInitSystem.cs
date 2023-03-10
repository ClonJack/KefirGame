using Asteroids.Components;
using Asteroids.Configuration;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _ecsWorld = default;
        private readonly EcsCustomInject<MainConfig> _mainConfig = default;

        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<PlayerData> _playerDataPool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<UnitData> _unitDataPool = default;
        private readonly EcsPoolInject<RigidbodyData> _rigiBodyDataPool = default;
        public void Init(IEcsSystems systems)
        {
            var entity = _ecsWorld.Value.NewEntity();
            
            _playerDataPool.Value.Add(entity);

            var player = Object.Instantiate(_mainConfig.Value.PlayerConfig.Prefab);

            ref var movementData = ref _movementDataPool.Value.Add(entity);
            movementData.MoveSpeed = _mainConfig.Value.PlayerConfig.MoveSpeed;
            movementData.RotationSpeed = _mainConfig.Value.PlayerConfig.RotationSpeed;

            ref var directionData = ref _directionDataPool.Value.Add(entity);
            directionData.Forward = player.transform.up;

            ref var unitData = ref _unitDataPool.Value.Add(entity);
            unitData.Unit = player.transform;

            ref var rigiBodyData = ref _rigiBodyDataPool.Value.Add(entity);
            rigiBodyData.Rigidbody = player.GetComponent<Rigidbody2D>();
        }
    }
}