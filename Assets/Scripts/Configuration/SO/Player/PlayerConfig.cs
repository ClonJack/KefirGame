using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Configs/Player", order = 0)]
    public class PlayerConfig : ScriptableObject
    {
        [Header("Prefab Unit")] [SerializeField]
        private GameObject _prefab;
        [Header("Transform Option")] [SerializeField]
        private float rotationSpeed;
        [SerializeField] 
        private float moveSpeed;
        [Header("Shot Option")] [SerializeField]
        private ModelShot _modelShot;
        public float RotationSpeed => rotationSpeed;
        public float MoveSpeed => moveSpeed;
        public GameObject Prefab => _prefab;
        public ModelShot ModelShot => _modelShot;
    }
}