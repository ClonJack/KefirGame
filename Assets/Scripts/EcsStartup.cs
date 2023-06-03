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
        
        private InputService _inputService = default;
        private EcsWorld _world = default;

        private IEcsSystems _systemUpdate = default;
        private IEcsSystems _systemsFixedUpdate = default;

        private void Awake()
        {
            _inputService = new InputService();
        }

        private void Start()
        {
            _world = new EcsWorld();
            _systemsFixedUpdate = new EcsSystems(_world, _mainConfig);
            _systemUpdate = new EcsSystems(_world, _mainConfig);
#if UNITY_EDITOR
            _systemsFixedUpdate.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem());
#endif

            _systemUpdate
                    
                .Add(new PlayerInputSystem())
                .Add(new ChangeWeaponSystem())
                .Add(new AttackTriggerSystem())
                .Add(new WeaponReloadSystem())
                .Add(new ShotSystem())
                .Add(new AmmoLifeTimeSystem())
                
                .DelHere<ChangeAction>()
                .DelHere<AttackAction>() 
                .DelHere<ReloadAction>()
                .DelHere<ShotAction>()
                
                .Inject(_mainConfig)
                .Inject(_inputService)
                .Inject(_poolServices)
                
                .Init();

            _systemsFixedUpdate
                .Add(new PlayerInitSystem())
           //     .Add(new PlanetInitSystem())
                .Add(new SceneInitSystem())
             //   .Add(new PlanetMoveSystem())
                .Add(new MovementSystem())
                .Add(new RotationSystem())
                .Add(new BoundsCameraSystem())
               
                .DelHere<MoveAction>()
                .DelHere<PlanetAction>()
                
                .Inject(_mainConfig)
                .Inject(_poolServices)
                
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