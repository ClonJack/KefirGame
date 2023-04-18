using System.Linq;
using Asteroids.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace ECS.Systems
{
    public class SceneInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var viewConverters = Object
                .FindObjectsOfType<MonoBehaviour>()
                .OfType<IConverter>();

            foreach (var converter in viewConverters)
            {
                converter.Convert(systems.GetWorld());
            }
        }
    }
}