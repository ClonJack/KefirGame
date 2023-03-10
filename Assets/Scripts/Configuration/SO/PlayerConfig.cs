using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Prefab")] [SerializeField] private GameObject _prefab;

        [Header("Transform Option")] [SerializeField]
        private float rotationSpeed;

        [SerializeField] private float moveSpeed;

        public float RotationSpeed => rotationSpeed;
        public float MoveSpeed => moveSpeed;

        public GameObject Prefab => _prefab;
    }
}