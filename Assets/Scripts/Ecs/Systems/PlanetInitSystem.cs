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

        private Vector2 _maxBoundCamera = default;
        private Vector2 _minBoundCamera = default;

        public void Init(IEcsSystems systems)
        {
            _maxBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.one);
            _minBoundCamera = Camera.main.ViewportToWorldPoint(Vector2.zero);

            for (var i = 0; i < _mainConfig.Value.MaxPlanet; i++)
            {
                var planet = _servicesRefPool.Value.PlanetPool.GetPool().Get();

                var rand = new Vector3(Random.Range(_minBoundCamera.x, _maxBoundCamera.x),
                    Random.Range(_minBoundCamera.y, _maxBoundCamera.y));

                planet.transform.position = rand;
            }
        }
    }
}