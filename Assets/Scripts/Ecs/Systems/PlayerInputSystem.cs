using Asteroids.Components;
using InputControl;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInputSystem :  IEcsRunSystem
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
                UpdateAmmo(entity);
                UpdateShot(entity);
            }
        }
        private void UpdateShot(int entity)
        {
            if (!_weaponDataPool.Value.Has(entity)) return;
            ref var weaponData = ref _weaponDataPool.Value.Get(entity);

            foreach (var ecsPackedEntity in weaponData.EcsPackedEntities)
            {
                var isUnPack = ecsPackedEntity.Unpack(_ecsWorld.Value, out var unpackedEntity);

                if (_inputServicePool.Value.IsShot)
                {
                    if (isUnPack && !_attackActionPool.Value.Has(unpackedEntity))
                    {
                        _attackActionPool.Value.Add(unpackedEntity);
                    }
                    continue;
                }
                if (_attackActionPool.Value.Has(unpackedEntity))
                    _attackActionPool.Value.Del(unpackedEntity);
            }
        }
        private void UpdateAmmo(int entity)
        {
            if (!_weaponDataPool.Value.Has(entity)) return;
            ref var weaponData = ref _weaponDataPool.Value.Get(entity);

            foreach (var ecsPackedEntity in weaponData.EcsPackedEntities)
            {
                var isUnPack = ecsPackedEntity.Unpack(_ecsWorld.Value, out var unpackedEntity);

                if (_inputServicePool.Value.IsChangeWeapon)
                {
                    if (isUnPack && !_ammoActionPool.Value.Has(unpackedEntity))
                    {
                        _ammoActionPool.Value.Add(unpackedEntity);
                    }
                    continue;
                }
                if (_ammoActionPool.Value.Has(unpackedEntity))
                    _ammoActionPool.Value.Del(unpackedEntity);
            }
            
            
            
        }
        private void UpdateDirection(int entity)
        {
            ref var directionData = ref _directionDataPool.Value.Get(entity);
            directionData.Direction = new Vector3(_inputServicePool.Value.Axis.x, _inputServicePool.Value.Axis.y);
        }
    }
}