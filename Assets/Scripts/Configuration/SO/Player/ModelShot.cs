using UnityEngine;

namespace Asteroids.Configuration
{
    [System.Serializable]
    public class ModelShot
    {
        [Header("Option")] 
        public float CoolDown;
        public float Damage;
        public float Speed;
    }
}