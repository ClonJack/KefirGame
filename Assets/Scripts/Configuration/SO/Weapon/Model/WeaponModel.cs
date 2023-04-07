using Configuration;
using UnityEngine;

namespace Asteroids.Configuration
{
    [System.Serializable]
    public class WeaponModel
    {
        [Header("Prefab")]
        [SerializeField] private Sprite _weapon;
        [Header("Ability Config")]
        [SerializeField] private AbilityConfig _ability;
        
        public Sprite Weapon => _weapon;
        public AbilityConfig Ability => _ability;
    }
}