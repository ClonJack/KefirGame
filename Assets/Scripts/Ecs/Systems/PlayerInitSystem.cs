using System.Collections.Generic;
using Asteroids.Components;
using Asteroids.Configuration;
using Asteroids.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _ecsWorld = default;
        private readonly EcsCustomInject<MainConfig> _mainConfig = default;

        private readonly EcsPoolInject<Player> _playerDataPool = default;
        private readonly EcsPoolInject<Unit> _unitDataPool = default;

        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<WeaponData> _weaponDataPool = default;

        private readonly EcsPoolInject<ComponentRef<Rigidbody2D>> _rigiBodyRefPool = default;
        private readonly EcsPoolInject<ComponentRef<Transform>> _transformRefPool = default;


        public void Init(IEcsSystems systems)
        {
            var entity = _ecsWorld.Value.NewEntity();

            _playerDataPool.Value.Add(entity);
            _unitDataPool.Value.Add(entity);
            _directionDataPool.Value.Add(entity);

            var player = Object.Instantiate(_mainConfig.Value.PlayerConfig.Prefab);

            ref var movementData = ref _movementDataPool.Value.Add(entity);
            movementData.MoveSpeed = _mainConfig.Value.PlayerConfig.MoveSpeed;
            movementData.RotationSpeed = _mainConfig.Value.PlayerConfig.RotationSpeed;

            ref var rigiBodyRef = ref _rigiBodyRefPool.Value.Add(entity);
            rigiBodyRef.Component = player.GetComponent<Rigidbody2D>();

            ref var transformRef = ref _transformRefPool.Value.Add(entity);
            transformRef.Component = player.transform;

            ref var weaponData = ref _weaponDataPool.Value.Add(entity);
            weaponData.EcsPackedEntities = new List<EcsPackedEntity>();
            
            var bindings = player.GetComponentsInChildren<IBinding<List<EcsPackedEntity>>>();
            foreach (var binding in bindings)
            {
                binding.Bind(_ecsWorld.Value,weaponData.EcsPackedEntities);
            }
        }
    }
}