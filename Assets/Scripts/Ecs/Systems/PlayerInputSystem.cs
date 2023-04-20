using Asteroids.Components;
using InputControl;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _ecsWorld = default;

        private readonly EcsFilterInject<Inc<Player>> _filter = default;

        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<AmmoAction> _ammoActionPool = default;
        private readonly EcsPoolInject<WeaponData> _weaponDataPool = default;

        private readonly EcsCustomInject<IInputService> _inputServicePool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                UpdateDirection(entity);

                if (!_weaponDataPool.Value.Has(entity)) continue;

                ChangeWeapon(entity);
                
                UpdateShot(entity);
            }
        }
        private void UpdateShot(int entity)
        {
            ref var weaponData = ref _weaponDataPool.Value.Get(entity);

            foreach (var ecsPackedEntity in weaponData.EcsPackedEntities)
            {
                var isUnPack = ecsPackedEntity.Unpack(_ecsWorld.Value, out var unpackedEntity);

                if (_inputServicePool.Value.IsShot && isUnPack && !_attackActionPool.Value.Has(unpackedEntity))
                {
                    _attackActionPool.Value.Add(unpackedEntity);
                }
            }
        }
        private void ChangeWeapon(int entity)
        {
            ref var weaponData = ref _weaponDataPool.Value.Get(entity);

            foreach (var ecsPackedEntity in weaponData.EcsPackedEntities)
            {
                var isUnPack = ecsPackedEntity.Unpack(_ecsWorld.Value, out var unpackedEntity);

                if (_inputServicePool.Value.IsChangeWeapon && isUnPack && !_ammoActionPool.Value.Has(unpackedEntity))
                {
                    _ammoActionPool.Value.Add(unpackedEntity);
                }
            }
        }
        private void UpdateDirection(int entity)
        {
            ref var directionData = ref _directionDataPool.Value.Get(entity);
            directionData.Direction = new Vector3(_inputServicePool.Value.Axis.x, _inputServicePool.Value.Axis.y);
        }
    }
}