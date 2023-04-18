using Asteroids.Components;
using Asteroids.Configuration;
using Configuration;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public class ChangeAmmoSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AmmoAction>> _filter = default;

        private readonly EcsPoolInject<AmmoSpriteRef> _ammoSpriteRefPool = default;
        private readonly EcsPoolInject<AbilityData> _abilityDataPool = default;
        private readonly EcsPoolInject<IndexAmmoData> _indexAmmoDataPool = default;

        private readonly EcsCustomInject<MainConfig> _mainConfig = default;

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var ability = ref _abilityDataPool.Value.Get(entity);
                ref var ammoSpite = ref _ammoSpriteRefPool.Value.Get(entity);
                ref var indexAmmo = ref _indexAmmoDataPool.Value.Get(entity);

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