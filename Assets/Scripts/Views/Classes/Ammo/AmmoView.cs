using UnityEngine;

namespace Asteroids.ECS.Views
{
    public class AmmoView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _icon;
        [SerializeField] private Rigidbody2D _rigidbody;

        public Rigidbody2D Rigidbody => _rigidbody;

        public void SetIcon(Sprite sprite)
        {
            _icon.sprite = sprite;
        }
    }
}