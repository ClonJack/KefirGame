using Configuration;
using UnityEngine;

namespace Asteroids.Configuration
{
    [System.Serializable]
    public class EmptyModel
    {
        [Header("Prefab")]
        [SerializeField] private Sprite _icon;
        [Header("Ability Config")]
        [SerializeField] private AbilityConfig _ability;
        
        public Sprite Icon => _icon;
        public AbilityConfig Ability => _ability;
    }
}