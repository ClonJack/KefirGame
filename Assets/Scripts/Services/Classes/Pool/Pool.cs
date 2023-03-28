using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Services
{
    [System.Serializable]
    public class Pool<T> where T : Component
    {
        [SerializeField] private T _instance;

        private IObjectPool<T> _pool;

        public IObjectPool<T> GetPool()
        {
            return _pool ??= new ObjectPool<T>(CreatePooledItem, OnTakeFromPool,
                OnReturnedToPool,
                OnDestroyPoolObject);
        }

        private void OnDestroyPoolObject(T param) => Object.Destroy(param.gameObject);
        private void OnReturnedToPool(T param) => param.gameObject.SetActive(false);
        private void OnTakeFromPool(T param) => param.gameObject.SetActive(true);
        private T CreatePooledItem() => Object.Instantiate(_instance);
    }
}