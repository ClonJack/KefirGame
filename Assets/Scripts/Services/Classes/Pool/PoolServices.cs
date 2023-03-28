using UnityEngine;

namespace Asteroids.Services
{
    public class PoolServices : MonoBehaviour
    {
        [SerializeField] private Pool<Rigidbody2D> _lazerPool = default;
        [SerializeField] private Pool<Rigidbody2D> _ballPool = default;
        public Pool<Rigidbody2D> LazerPool => _lazerPool;
        public Pool<Rigidbody2D> BallPool => _ballPool;
        
    }
}