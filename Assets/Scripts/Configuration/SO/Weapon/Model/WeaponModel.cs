using Configuration;
using UnityEngine;

namespace Asteroids.Configuration
{
    [System.Serializable]
    public class WeaponModel
    {
        [Header("Prefab")]
        [SerializeField] private GameObject _weapon;
        [Header("Ability Config")]
        [SerializeField] private AbilityConfig _ability;
        
        public GameObject Weapon => _weapon;
        public AbilityConfig Ability => _ability;
    }
}