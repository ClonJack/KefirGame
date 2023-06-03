using Asteroids.Components;
using Asteroids.Configuration;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class ShotSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShotAction>> _filter = default;

        private readonly EcsPoolInject<IndexAmmoData> _indexAmmoPool = default;
        private readonly EcsPoolInject<AmmoLifeTimeData> _ammoPool = default;
        private readonly EcsPoolInject<DirectionData> _directionPool = default;
        private readonly EcsPoolInject<MoveAction> _moveActionPool = default;
        private readonly EcsPoolInject<MovementData> _movementDataPool = default;
        private readonly EcsPoolInject<ComponentRef<Rigidbody2D>> _rigidbodyRefPool = default;

        private readonly EcsPoolInject<ComponentRef<Transform>> _pointRefPool = default;
        private readonly EcsPoolInject<AmmoSpriteRef> _ammoSpriteRefPool = default;

        private readonly EcsCustomInject<PoolServices> _servicesRefPool = default;
        private readonly EcsCustomInject<MainConfig> _mainConfig = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var pointRef = ref _pointRefPool.Value.Get(entity);
                ref var ammoSpriteRef = ref _ammoSpriteRefPool.Value.Get(entity);
                ref var indexData = ref _indexAmmoPool.Value.Get(entity);

                var bullet = _servicesRefPool.Value.AmmoPool.GetPool().Get();
                bullet.SetIcon(ammoSpriteRef.Sprite);

                bullet.transform.position = pointRef.Component.position;
                bullet.transform.rotation = pointRef.Component.rotation;

                var newEntity = systems.GetWorld().NewEntity();

                ref var direction = ref _directionPool.Value.Add(newEntity);
                direction.Direction = pointRef.Component.up;

                ref var ammoData = ref _ammoPool.Value.Add(newEntity);
                ammoData.Ammo = bullet;
                ammoData.Timer = _mainConfig.Value.WeaponsConfig.Models[indexData.Index].Ability.LifeTime;

                ref var movementData = ref _movementDataPool.Value.Add(newEntity);
                movementData.MoveSpeed = _mainConfig.Value.WeaponsConfig.Models[indexData.Index].Ability.Speed;

                ref var rigibodyRef = ref _rigidbodyRefPool.Value.Add(newEntity);
                rigibodyRef.Component = bullet.Rigidbody;

                _moveActionPool.Value.Add(newEntity);
            }
        }
    }
}