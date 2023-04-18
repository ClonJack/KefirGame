using Asteroids.Components;
using Asteroids.Configuration;
using Asteroids.Services;
using ECS.Systems;
using InputControl;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Asteroids.ECS
{
    sealed class EcsStartup : MonoBehaviour
    {
        [SerializeField] private MainConfig _mainConfig = default;
        [SerializeField] private PoolServices _poolServices = default;

        private InputGameControl _inputGameControl = default;
        private InputService _inputService = default;
        private EcsWorld _world = default;

        private IEcsSystems _systemUpdate = default;
        private IEcsSystems _systemsFixedUpdate = default;

        private void Awake()
        {
            _inputGameControl = new InputGameControl();
            _inputGameControl.Init();

            _inputService = new InputService(_inputGameControl);
        }

        private void Start()
        {
            _world = new EcsWorld();
            _systemsFixedUpdate = new EcsSystems(_world);
            _systemUpdate = new EcsSystems(_world);
#if UNITY_EDITOR
            _systemsFixedUpdate.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _systemUpdate
                .Add(new PlayerInputSystem())
                .Add(new ChangeAmmoSystem())
                .Add(new AttackTriggerSystem())
                .Add(new AmmoLifeTimeSystem())
                .Inject(_mainConfig)
                .Inject(_inputService)
                .Inject(_poolServices)
                .Init();

            _systemsFixedUpdate
                .Add(new PlayerInitSystem())
                .Add(new SceneInitSystem())
                .Add(new MovementSystem())
                .Add(new BoundsCameraSystem())
                .Add(new AttackSystem())
                .DelHere<ShotData>()
                .Inject(_mainConfig)
                .Inject(_poolServices)
                .Init();
        }

        private void Update()
        {
            _systemUpdate?.Run();
            _inputService?.Update();
        }

        private void FixedUpdate()
        {
            _systemsFixedUpdate?.Run();
        }

        private void OnEnable()
        {
            _inputGameControl.Enable();
        }

        private void OnDestroy()
        {
            _inputGameControl.Disable();
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