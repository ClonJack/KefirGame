using Asteroids.Components;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AbilityData, AttackAction, Player, ShotData, ComponentRef<Transform>>>
            _filter =
                default;

        private readonly EcsPoolInject<ShotData> _shotDataPool = default;
        private readonly EcsPoolInject<AbilityData> _abilityDataPool = default;
        private readonly EcsPoolInject<AttackAction> _attackActionPool = default;
        private readonly EcsPoolInject<Player> _playerPool = default;
        private readonly EcsPoolInject<AmmoLifeTimeData> _ammoPool = default;

        private readonly EcsPoolInject<ComponentRef<Transform>> _pointRefPool = default;

        private readonly EcsCustomInject<PoolServices> _servicesRefPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var abilityData = ref _abilityDataPool.Value.Get(entity);
                ref var attackAction = ref _attackActionPool.Value.Get(entity);
                ref var playerData = ref _playerPool.Value.Get(entity);
                ref var pointRef = ref _pointRefPool.Value.Get(entity);
                ref var shotData = ref _shotDataPool.Value.Get(entity);

                var bullet = _servicesRefPool.Value.BallPool.GetPool().Get();
                bullet.transform.position = pointRef.Component.position;
                bullet.AddRelativeForce(pointRef.Component.up * (abilityData.Speed * Time.fixedDeltaTime));
                
                ref var ammoData = ref _ammoPool.Value.Add(systems.GetWorld().NewEntity());
                ammoData.Ammo = bullet;
                ammoData.Timer = 2;
            }
        }
    }
}