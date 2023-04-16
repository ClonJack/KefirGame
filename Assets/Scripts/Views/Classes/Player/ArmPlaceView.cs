using System.Collections.Generic;
using Asteroids.Components;
using Asteroids.Configuration;
using Leopotam.EcsLite;
using UnityEngine;

namespace Asteroids.Views
{
    public class ArmPlaceView : MonoBehaviour, IConverter
    {
        [SerializeField] private WeaponConfig _weaponConfig;

        public void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities)
        {
            var entity = ecsWorld.NewEntity();

            ref var ability = ref ecsWorld.GetPool<AbilityData>().Add(entity);
            ability = _weaponConfig.CurrentModel.Ability.Convert();

            ref var spriteRef = ref ecsWorld.GetPool<AmmoSpriteRef>().Add(entity);
            spriteRef.Sprite = _weaponConfig.CurrentModel.Weapon;

            ref var componentRef = ref ecsWorld.GetPool<ComponentRef<Transform>>().Add(entity);
            componentRef.Component = transform;

            ref var player = ref ecsWorld.GetPool<Player>().Add(entity);

            ref var configWeapon = ref ecsWorld.GetPool<ConfigWeaponRef>().Add(entity);
            configWeapon.WeaponConfig = Instantiate(_weaponConfig);

            entities.Add(gameObject, entity);
        }
    }
}