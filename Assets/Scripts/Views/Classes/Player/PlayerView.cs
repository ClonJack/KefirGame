using System.Collections.Generic;
using Asteroids.Components;
using Leopotam.EcsLite;
using UnityEngine;

namespace Asteroids.Views
{
    public class PlayerView : MonoBehaviour, IConverter
    {
        [SerializeField] private ParticleSystem _leftGun;
        [SerializeField] private ParticleSystem _rightGun;
        public void Convert(EcsWorld ecsWorld, Dictionary<GameObject, int> entities)
        {
            var filter = ecsWorld.Filter<Player>().End();
            foreach (var entity in filter)
            {
                ref var pointsRef = ref ecsWorld.GetPool<PointsRef>().Add(entity);
                pointsRef.Points = new List<Transform>();
                pointsRef.Points.Add(_leftGun.transform);
                pointsRef.Points.Add(_rightGun.transform);
            }
           
        }
    }
}