using UnityEngine;

namespace PowerTrip
{
    [RequireComponent(typeof(Transform))]
    public class SpriteRotator : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _sprite;
        
        private Transform _transform;
        private float _lastAxis = 0f;
        
        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (IsMovingRightUnflipped())
                _sprite.flipX = false;
            else if (IsMovingLeftUnflipped())
                _sprite.flipX = true;

            _lastAxis = _transform.position.x;
        }

        private bool IsMovingRightUnflipped()
        {
            return _lastAxis - _transform.position.x < 0 && _sprite.flipX;
        }

        private bool IsMovingLeftUnflipped()
        {
            return _lastAxis - _transform.position.x > 0 && !_sprite.flipX;
        }
    }
}