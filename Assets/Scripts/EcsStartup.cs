using System;
using Asteroids.Configuration;
using ECS.Systems;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Asteroids.ECS
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private MainConfig _mainConfig = default;

        private PlayerInput _playerInput = default;
        private EcsWorld _world = default;
        private IEcsSystems _systems = default;

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
#if UNITY_EDITOR
            _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _systems
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new MovementSystem())
                
                
                .Inject(_mainConfig)
                .Inject(_playerInput)
                
                .Init();
        }


        private void OnEnable()
        {
            _playerInput = new PlayerInput();
            _playerInput.Enable();
        }


        private void FixedUpdate()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }

            _playerInput.Disable();
        }
    }
}