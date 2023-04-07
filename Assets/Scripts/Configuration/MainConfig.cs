using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Configs/Main Configuration", order = 0)]
    public class MainConfig : ScriptableObject
    {
        [SerializeField] private PlayerConfig _playerConfig;
        [SerializeField] private WeaponConfig _weaponConfig;
        public PlayerConfig PlayerConfig => _playerConfig;
        public WeaponConfig WeaponConfig => _weaponConfig;
    }
}