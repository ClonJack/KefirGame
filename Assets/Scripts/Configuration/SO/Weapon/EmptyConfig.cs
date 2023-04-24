using System.Collections.Generic;
using UnityEngine;

namespace Asteroids.Configuration
{
    [CreateAssetMenu(fileName = "Config", menuName = "Configs/Config", order = 0)]
    public class EmptyConfig : ScriptableObject
    {
        [SerializeField] private List<EmptyModel> _models;
        public List<EmptyModel> Models => _models;
    }
}