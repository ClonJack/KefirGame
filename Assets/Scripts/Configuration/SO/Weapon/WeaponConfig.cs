using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponModel> _weaponModels;
        public List<WeaponModel> WeaponModels => _weaponModels;
        
    }
}