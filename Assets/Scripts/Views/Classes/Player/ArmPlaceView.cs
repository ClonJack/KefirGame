using System.Collections.Generic;
using Asteroids.Components;
using Asteroids.Configuration;
using Configuration;
using Leopotam.EcsLite;
using UnityEngine;

namespace Asteroids.Views
{
    public class ArmPlaceView : MonoBehaviour, IConverter, IBinding<List<EcsPackedEntity>>
    {
        [SerializeField] private WeaponConfig _weaponConfig;

        private List<EcsPackedEntity> _packedEntities;

        public void Convert(EcsWorld ecsWorld)
        {
            var entity = ecsWorld.NewEntity();

            ref var ability = ref ecsWorld.GetPool<AbilityData>().Add(entity);
            ability = _weaponConfig.WeaponModels[0].Ability.Convert();

            ref var indexAmmo = ref ecsWorld.GetPool<IndexAmmoData>().Add(entity);
            indexAmmo.Index = 0;

            ref var spriteRef = ref ecsWorld.GetPool<AmmoSpriteRef>().Add(entity);
            spriteRef.Sprite = _weaponConfig.WeaponModels[0].Weapon;

            ref var componentRef = ref ecsWorld.GetPool<ComponentRef<Transform>>().Add(entity);
            componentRef.Component = transform;

            _packedEntities.Add(ecsWorld.PackEntity(entity));
        }

        public void Bind(EcsWorld ecsWorld, List<EcsPackedEntity> param)
        {
            _packedEntities = param;
        }
    }
}