using Asteroids.Components;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShotData>> _filter = default;
        
        private readonly EcsPoolInject<AbilityData> _abilityDataPool = default;
        private readonly EcsPoolInject<AmmoLifeTimeData> _ammoPool = default;

        private readonly EcsPoolInject<ComponentRef<Transform>> _pointRefPool = default;
        private readonly EcsPoolInject<AmmoSpriteRef> _ammoSpriteRefPool = default;

        private readonly EcsCustomInject<PoolServices> _servicesRefPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var abilityData = ref _abilityDataPool.Value.Get(entity);
                ref var pointRef = ref _pointRefPool.Value.Get(entity);
                ref var ammoSpriteRef = ref _ammoSpriteRefPool.Value.Get(entity);

                var bullet = _servicesRefPool.Value.AmmoViewPool.GetPool().Get();
                bullet.SetIcon(ammoSpriteRef.Sprite);

                bullet.transform.position = pointRef.Component.position;
                bullet.transform.rotation = pointRef.Component.rotation;

                bullet.Rigidbody.AddForce(pointRef.Component.up * (abilityData.Speed * Time.fixedDeltaTime));

                ref var ammoData = ref _ammoPool.Value.Add(systems.GetWorld().NewEntity());
                ammoData.Ammo = bullet;
                ammoData.Timer = abilityData.AmmoLifeTime;
            }
        }
    }
}