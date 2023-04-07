using System.Collections.Generic;
using Asteroids.Components;
using Asteroids.Configuration;
using Leopotam.EcsLite;
using UnityEngine;

namespace Asteroids.Views
{
    public class ArmPlaceView : MonoBehaviour, IConverter
    {
        [SerializeField] private WeaponModel _weaponModel;
        public void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities)
        {
            var entity = ecsWorld.NewEntity();
            ref var ability = ref ecsWorld.GetPool<AbilityData>().Add(entity);
            ability = _weaponModel.Ability.Convert();

            ref var spriteRef = ref ecsWorld.GetPool<AmmoSpriteRef>().Add(entity);
            spriteRef.Sprite = _weaponModel.Weapon;
            
            ref var componentRef = ref ecsWorld.GetPool<ComponentRef<Transform>>().Add(entity);
            componentRef.Component = transform;
            entities.Add(gameObject, entity);
            ref var player = ref ecsWorld.GetPool<Player>().Add(entity);
        }
    }
}