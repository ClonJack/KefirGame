using Asteroids.Components;
using InputControl;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlayerInputSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<Player, DirectionData>> _filterMove = default;
        private readonly EcsFilterInject<Inc<Player, AbilityData>> _filterAmmo = default;

        private readonly EcsCustomInject<IInputService> _inputServicePool = default;
        private readonly EcsPoolInject<DirectionData> _directionDataPool = default;
        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<AmmoAction> _ammoActionPool = default;

        private void OnShot(bool isShot)
        {
            foreach (var entity in _filterAmmo.Value)
            {
                if (isShot)
                {
                    if (!_attackActionPool.Value.Has(entity))
                        _attackActionPool.Value.Add(entity);
                    continue;
                }

                _attackActionPool.Value.Del(entity);
            }
        }

        private void OnAxis(Vector2 axis)
        {
            foreach (var entity in _filterMove.Value)
            {
                ref var directionData = ref _directionDataPool.Value.Get(entity);
                directionData.Direction = directionData.Forward * axis.y;
                directionData.Rotation = Vector3.forward * axis.x;
            }
        }

        private void OnNextWeapon(bool isChangeWeapon)
        {
            foreach (var entity in _filterAmmo.Value)
            {
                if (isChangeWeapon)
                {
                    if (!_ammoActionPool.Value.Has(entity))
                        _ammoActionPool.Value.Add(entity);
                    continue;
                }

                _ammoActionPool.Value.Del(entity);
            }
        }

        public void Run(IEcsSystems systems)
        {
            OnAxis(_inputServicePool.Value.Axis);
            OnShot(_inputServicePool.Value.IsShot);
            OnNextWeapon(_inputServicePool.Value.IsChangeWeapon);
        }
    }
}