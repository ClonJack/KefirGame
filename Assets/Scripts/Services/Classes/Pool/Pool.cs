using UnityEngine;
using UnityEngine.Pool;

namespace Asteroids.Services
{
    [System.Serializable]
    public class AmmoPool 
    {
        private IObjectPool<GameObject> _pool;
        [SerializeField] private GameObject _ammoParticle;

        public IObjectPool<GameObject> GetPool()
        {
            if (_pool == null)
            {
                _pool = new ObjectPool<GameObject>(CreatePooledItem, OnTakeFromPool,
                    OnReturnedToPool,
                    OnDestroyPoolObject);
            }

            return _pool;
        }

        private void OnDestroyPoolObject(GameObject param) => Object.Destroy(param.gameObject);
        private void OnReturnedToPool(GameObject param) => param.gameObject.SetActive(false);
        private void OnTakeFromPool(GameObject param) => param.gameObject.SetActive(true);
        private GameObject CreatePooledItem() => Object.Instantiate(_ammoParticle);
    }
}