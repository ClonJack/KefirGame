using Asteroids.Components;
using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "NameAbility", menuName = "Configs/Ability", order = 0)]
    public class AbilityConfig : ScriptableObject, IConfigConvert<AbilityData>
    {
        [SerializeField] private GameObject _prefabAmmo;

        [SerializeField] private float _coolDown;
        [SerializeField] private int _speed;
        [SerializeField] private int _damage;
        public AbilityData Convert() => new(_coolDown, _speed, _damage, 0);
    }
}