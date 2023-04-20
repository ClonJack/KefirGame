using Asteroids.Components;
using Asteroids.ECS.Ecs.Extension;
using InputControl;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player>> _filter = default;

        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<AmmoAction> _ammoActionPool = default;
        
        private readonly EcsPoolInject<Player> _playerDataPool;

        private readonly EcsCustomInject<IInputService> _inputServicePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                UpdateDirection(entity);
                
                ChangeWeapon(entity);

                UpdateShot(entity);
            }
        }

        private void UpdateShot(int entity)
        {
            if (_inputServicePool.Value.IsShot)
                _attackActionPool.Value.AddUnique(entity);
        }

        private void ChangeWeapon(int entity)
        {
            if (_inputServicePool.Value.IsChangeWeapon)
                _ammoActionPool.Value.AddUnique(entity);
        }

        private void UpdateDirection(int entity)
        {
            ref var directionData = ref _directionDataPool.Value.Get(entity);
            directionData.Direction = new Vector3(_inputServicePool.Value.Axis.x, _inputServicePool.Value.Axis.y);
        }
    }
}