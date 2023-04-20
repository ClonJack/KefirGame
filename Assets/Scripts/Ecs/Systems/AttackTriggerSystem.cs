using Asteroids.Components;
using Asteroids.ECS.Ecs.Extension;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public class AttackTriggerSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackAction>> _filter = default;

        private readonly EcsPoolInject<AttackProcessAction> _attackProcessPool = default;

        private readonly EcsPoolInject<WeaponData> _weaponDataPool = default;
        
        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var weaponData = ref _weaponDataPool.Value.Get(entity);
                foreach (var ecsPackedEntity in weaponData.EcsPackedEntities)
                {
                    var entityUnpackacked = ecsPackedEntity.Unpack(systems.GetWorld(), out var indexUnpacked);
                    if (!entityUnpackacked) continue;

                    _attackProcessPool.Value.AddUnique(indexUnpacked);
                }
            }
        }
    }
}