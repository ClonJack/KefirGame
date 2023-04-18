using Asteroids.Components;
using Asteroids.Configuration;

namespace Configuration
{
    public static class ConfigExtension
    {
        public static AbilityData Convert(this AbilityConfig abilityConfig) =>
            new(abilityConfig.Speed, abilityConfig.Damage, abilityConfig.LifeTime);

        public static int GetIndex(this WeaponConfig weaponConfig, WeaponModel weaponModel) =>
            weaponConfig.WeaponModels.IndexOf(weaponModel);
    }
}