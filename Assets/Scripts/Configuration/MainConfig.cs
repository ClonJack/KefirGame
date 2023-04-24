using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "MainConfig", menuName = "Configs/Main Configuration", order = 0)]
    public class MainConfig : ScriptableObject
    {
        [Header("Player")]
        [SerializeField] private PlayerConfig _playerConfig;
        
        [Header("Weapon")]
        [SerializeField] private EmptyConfig _weaponsConfig;
        
        [Header("Planet")] 
        [SerializeField] private EmptyConfig _planetConfig;
        [SerializeField] private int _maxPlanet;
        [SerializeField] private float _coolDown;
        [SerializeField] private float _maxVelocity;
        public PlayerConfig PlayerConfig => _playerConfig;
        public EmptyConfig WeaponsConfig => _weaponsConfig;
        public EmptyConfig PlanetConfig => _planetConfig;
        public int MaxPlanet => _maxPlanet;
        public float CoolDown => _coolDown;
        public float MaxVelocity => _maxVelocity;
    }
}