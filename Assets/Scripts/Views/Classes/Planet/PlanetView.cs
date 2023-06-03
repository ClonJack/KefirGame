using Asteroids.Components;
using Asteroids.Configuration;
using Asteroids.Views;
using Configuration;
using Leopotam.EcsLite;
using UnityEngine;

namespace Asteroids.ECS.Views
{
    public class PlanetView : MonoBehaviour, IConverter
    {
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private int _entity;
        public int Entity => _entity;
        public void Convert(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            _entity = ecsWorld.NewEntity();

            var planetConfig = ecsSystems.GetShared<MainConfig>().PlanetConfig;

            ref var indexPlanet = ref ecsWorld.GetPool<IndexAmmoData>().Add(_entity);
            indexPlanet.Index = Random.Range(0, planetConfig.Models.Count);

            ref var ability = ref ecsWorld.GetPool<AbilityData>().Add(_entity);
            ability = planetConfig.Models[indexPlanet.Index].Ability.Convert();

            ref var spriteRef = ref ecsWorld.GetPool<AmmoSpriteRef>().Add(_entity);
            spriteRef.Sprite = planetConfig.Models[indexPlanet.Index].Icon;
            _icon.sprite = spriteRef.Sprite;

            ref var componentRef = ref ecsWorld.GetPool<ComponentRef<Transform>>().Add(_entity);
            componentRef.Component = transform;

            ref var rigibody = ref ecsWorld.GetPool<ComponentRef<Rigidbody2D>>().Add(_entity);
            rigibody.Component = _rigidbody;

            ref var movementData = ref ecsWorld.GetPool<MovementData>().Add(_entity);
            movementData.MoveSpeed = ability.Speed;
            
            ecsWorld.GetPool<DirectionData>().Add(_entity);
            ecsWorld.GetPool<PlanetAction>().Add(_entity);
        }
    }
}