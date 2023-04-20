using Asteroids.Components;
using Asteroids.Configuration;
using Configuration;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public class ChangeWeaponSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ChangeAction>> _filter = default;

        private readonly EcsPoolInject<AmmoSpriteRef> _ammoSpriteRefPool = default;
        private readonly EcsPoolInject<AbilityData> _abilityDataPool = default;
        private readonly EcsPoolInject<IndexAmmoData> _indexAmmoDataPool = default;
        private readonly EcsPoolInject<WeaponData> _weaponDataPool = default;

        private readonly EcsCustomInject<MainConfig> _mainConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var weaponData = ref _weaponDataPool.Value.Get(entity);

                foreach (var ecsPackedEntity in weaponData.EcsPackedEntities)
                {
                    if (ecsPackedEntity.Unpack(systems.GetWorld(), out var entityUnPack))
                    {
                        ref var ability = ref _abilityDataPool.Value.Get(entityUnPack);
                        ref var ammoSpite = ref _ammoSpriteRefPool.Value.Get(entityUnPack);
                        ref var indexAmmo = ref _indexAmmoDataPool.Value.Get(entityUnPack);

                        indexAmmo.Index++;
                        if (indexAmmo.Index > _mainConfig.Value.WeaponConfig.WeaponModels.Count - 1)
                        {
                            indexAmmo.Index = 0;
                        }

                        var currentModel = _mainConfig.Value.WeaponConfig.WeaponModels[indexAmmo.Index];

                        ability = currentModel.Ability.Convert();
                        ammoSpite.Sprite = currentModel.Weapon;
                    }
                }
            }
        }
    }
}