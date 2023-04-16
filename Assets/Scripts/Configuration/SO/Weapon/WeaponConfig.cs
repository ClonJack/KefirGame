using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "WeaponConfig", menuName = "Configs/Weapon", order = 0)]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField] private List<WeaponModel> _weaponModels;
        [SerializeField] private int _currentIndex;
        public WeaponModel CurrentModel => _weaponModels[_currentIndex];

        public void NexWeapon()
        {
            _currentIndex++;
            if (_currentIndex > _weaponModels.Count - 1)
            {
                _currentIndex = 0;
            }
        }
    }
}