using Asteroids.ECS.Views.Classes.Ammo;
using UnityEngine;

namespace Asteroids.Services
{
    public class PoolServices : MonoBehaviour
    {
        [SerializeField] private Pool<AmmoView> _ammoViewPool;
        public Pool<AmmoView> AmmoViewPool => _ammoViewPool;
    }
}