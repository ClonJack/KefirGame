using UnityEngine;

namespace Configuration
{
    [CreateAssetMenu(fileName = "NameAbility", menuName = "Configs/Ability", order = 0)]
    public class AbilityConfig : ScriptableObject
    {
        [SerializeField] private float _coolDown;
        [SerializeField] private int _speed;
        [SerializeField] private int _damage;
        [SerializeField] private int _lifeTime;
        public  float CoolDown => _coolDown;
        public int Speed => _speed;
        public int Damage => _damage;
        public int LifeTime => _lifeTime;
    }
}