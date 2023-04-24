using Asteroids.Components;
using Asteroids.Configuration;

namespace Configuration
{
    public static class ConfigExtension
    {
        public static AbilityData Convert(this AbilityConfig abilityConfig) =>
            new(abilityConfig.Speed, abilityConfig.Damage);

        public static int GetIndex(this EmptyConfig emptyConfig, EmptyModel emptyModel) =>
            emptyConfig.Models.IndexOf(emptyModel);
    }
}