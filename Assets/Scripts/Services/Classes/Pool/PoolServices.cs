using Asteroids.ECS.Views;
using UnityEngine;

namespace Asteroids.Services
{
    public class PoolServices : MonoBehaviour
    {
        [SerializeField] private Pool<AmmoView> _ammoPool;
        [SerializeField] private Pool<PlanetView> _planetPool;
        public Pool<AmmoView> AmmoPool => _ammoPool;
        public Pool<PlanetView> PlanetPool => _planetPool;
    }
}