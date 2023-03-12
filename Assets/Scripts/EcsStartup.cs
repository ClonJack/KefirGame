using System;
using Asteroids.Configuration;
using Asteroids.Services;
using ECS.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Asteroids.ECS
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private MainConfig _mainConfig = default;

        private InputService _inputService = default;
        private EcsWorld _world = default;
        private IEcsSystems _systemsFixedUpdate = default;
        private IEcsSystems _systemUpdate = default;


        private void Start()
        {
            _inputService = new InputService();
            _inputService.Enable();

            _world = new EcsWorld();
            _systemsFixedUpdate = new EcsSystems(_world);
            _systemUpdate = new EcsSystems(_world);
#if UNITY_EDITOR
            _systemsFixedUpdate.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif


            _systemUpdate
                .Add(new PlayerInputSystem())
                .Inject(_inputService)
                .Init();

            _systemsFixedUpdate
                .Add(new PlayerInitSystem())
                .Add(new MovementSystem())
                .Add(new BoundsCameraSystem())
                .Inject(_mainConfig)
                .Init();
        }

        private void Update()
        {
            _systemUpdate?.Run();
        }

        private void FixedUpdate()
        {
            _systemsFixedUpdate?.Run();
        }


        private void OnDestroy()
        {
            _inputService.Disable();
            
            if (_systemsFixedUpdate != null)
            {
                _systemsFixedUpdate.Destroy();
                _systemsFixedUpdate = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}