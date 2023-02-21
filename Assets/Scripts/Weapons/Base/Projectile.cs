using UnityEngine;

namespace PowerTrip
{
    public class Projectile : MonoBehaviour, IDamaging
    {
        [SerializeField] private Transform _visual;
        
        private Vector3 _direction;
        private float _speed;
        private float _damage;

        private int _maxNumberOfCollisions = 1;
        private int _currentNumberOfCollisions;

        private bool _isInitialized;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
                DealDamage(damageable, _damage);
        }

        private void OnBecameInvisible()
        {
            // TODO: use pooling

            Destroy(gameObject);
        }

        private void Update()
        {
            if (!_isInitialized) return;
            
            transform.Translate(_direction * (_speed * Time.deltaTime));
        }

        public void Init(Vector3 direction, float speed, float damage)
        {
            _direction = direction;
            _speed = speed;
            _damage = damage;

            RotateToDirection();
            
            _isInitialized = true;
        }

        public void DealDamage(IDamageable damageable, float amout)
        {
            if (_currentNumberOfCollisions >= _maxNumberOfCollisions) return;

            _currentNumberOfCollisions++;

            damageable.GetDamage(_damage);

            if (_currentNumberOfCollisions >= _maxNumberOfCollisions)
            {
                // TODO: use pooling
                Destroy(gameObject);
            }
        }

        private void RotateToDirection()
        {
            if (_direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(_direction.y, _direction.x) * Mathf.Rad2Deg;
                _visual.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }
    }
}