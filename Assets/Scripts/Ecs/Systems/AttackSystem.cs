using Asteroids.Components;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class AttackSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ShotData, PointsRef, ComponentRef<Transform>, WeaponData>> _filter =
            default;

        private readonly EcsPoolInject<WeaponData> _weaponDataPool = default;

        private readonly EcsPoolInject<PointsRef> _pointsRefPool = default;
        private readonly EcsPoolInject<ComponentRef<Transform>> _transformRefPool = default;
        private readonly EcsCustomInject<PoolServices> _servicesRefPool = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var transformRef = ref _transformRefPool.Value.Get(entity);
                ref var pointsRef = ref _pointsRefPool.Value.Get(entity);

                ref var weaponData = ref _weaponDataPool.Value.Get(entity);

                var getRandom = pointsRef.Points[Random.Range(0, pointsRef.Points.Count)]; // убрать рандом отсюда(???)
                var bullet = _servicesRefPool.Value.BallPool.GetPool().Get();
                bullet.transform.position = getRandom.position;
                bullet.AddRelativeForce(transformRef.Component.up * (weaponData.Speed * Time.fixedDeltaTime));
            }
        }
    }
}