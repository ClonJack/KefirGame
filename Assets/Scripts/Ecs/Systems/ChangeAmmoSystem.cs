using Asteroids.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace ECS.Systems
{
    public class ChangeAmmoSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AmmoAction, AbilityData, AmmoSpriteRef, Player, ConfigWeaponRef>> _filter =
            default;

        private readonly EcsPoolInject<AmmoSpriteRef> _ammoSpriteRefPool = default;
        private readonly EcsPoolInject<AbilityData> _abilityDataPool = default;
        private readonly EcsPoolInject<ConfigWeaponRef> _configWeaponRefPool = default;


        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                ref var ability = ref _abilityDataPool.Value.Get(entity);
                ref var ammoSpite = ref _ammoSpriteRefPool.Value.Get(entity);
                ref var configWeapon = ref _configWeaponRefPool.Value.Get(entity);

                configWeapon.WeaponConfig.NexWeapon();

                ability = configWeapon.WeaponConfig.CurrentModel.Ability.Convert();
                ability.Timer = ability.CoolDown;
                ammoSpite.Sprite = configWeapon.WeaponConfig.CurrentModel.Weapon;
            }
        }
    }
}