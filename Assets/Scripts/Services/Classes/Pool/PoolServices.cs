using Asteroids.ECS.Views;
using UnityEngine;

namespace Asteroids.Services
{
    public class PoolServices : MonoBehaviour
    {
        [SerializeField] private Pool<AmmoView> _ammoViewPool;
        [SerializeField] private Pool<PlanetView> _planetViewPool;
        public Pool<AmmoView> AmmoViewPool => _ammoViewPool;
        public Pool<PlanetView> PlanetViewPool => _planetViewPool;
    }
}