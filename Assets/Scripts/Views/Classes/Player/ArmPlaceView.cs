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
        private List<EcsPackedEntity> _packedEntities = default;

        public void Convert(IEcsSystems ecsSystems)
        {
            var ecsWorld = ecsSystems.GetWorld();
            var entity = ecsWorld.NewEntity();
            
            var weaponsConfig = ecsSystems.GetShared<MainConfig>().WeaponsConfig;

            ref var indexAmmo = ref ecsWorld.GetPool<IndexAmmoData>().Add(entity);
            indexAmmo.Index = 0;
            
            ref var ability = ref ecsWorld.GetPool<AbilityData>().Add(entity);
            ability = weaponsConfig.Models[indexAmmo.Index].Ability.Convert();

            ref var spriteRef = ref ecsWorld.GetPool<AmmoSpriteRef>().Add(entity);
            spriteRef.Sprite = weaponsConfig.Models[indexAmmo.Index].Icon;

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