using System.Collections.Generic;
using System.Linq;
using Asteroids.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class SceneInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _ecsWorld = default;
        public void Init(IEcsSystems systems)
        {
            var viewConverters = Object
                .FindObjectsOfType<MonoBehaviour>()
                .OfType<IConverter>();
            
            var entities = new Dictionary<GameObject, int>();
            foreach (var converter in viewConverters)
                converter.Convert(_ecsWorld.Value, entities);
        }
    }
}