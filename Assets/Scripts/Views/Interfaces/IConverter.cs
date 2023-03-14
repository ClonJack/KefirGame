using System.Collections.Generic;
using Leopotam.EcsLite;
using UnityEngine;

namespace Asteroids.Views
{
    public interface IConverter
    {
        void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities);
    }
}