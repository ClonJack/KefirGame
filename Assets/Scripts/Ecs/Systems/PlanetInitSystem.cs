using Asteroids.Configuration;
using Asteroids.Services;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class PlanetInitSystem : IEcsInitSystem
    {
        private readonly EcsCustomInject<PoolServices> _servicesRefPool = default;
        private readonly EcsCustomInject<MainConfig> _mainConfig = default;

        public void Init(IEcsSystems systems)
        {
            var maxBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.one);
            var minBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.zero);

            for (var i = 0; i < _mainConfig.Value.MaxPlanet; i++)
            {
                var planet = _servicesRefPool.Value.PlanetViewPool.GetPool().Get();

                var rand = new Vector3(Random.Range(minBoundCamera.x, maxBoundCamera.x),
                    Random.Range(minBoundCamera.y, maxBoundCamera.y));

                planet.transform.position = rand;
            }
        }
    }
}