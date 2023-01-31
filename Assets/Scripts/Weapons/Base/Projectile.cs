using Interfaces;
using UnityEngine;

namespace Weapons
{
    public class Projectile : MonoBehaviour
    {
        private float _speed;
        private Vector3 _direction;
        private float _damage;
        private int _maxNumberOfCollisions = 1;
        private int _currentNumberOfCollisions;

        private bool _isInitialized;

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
            _isInitialized = true;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamagable damagable))
            {
                if (_currentNumberOfCollisions >= _maxNumberOfCollisions) return;
                
                _currentNumberOfCollisions++;
                
                damagable.GetDamage(_damage);
                
                if (_currentNumberOfCollisions >= _maxNumberOfCollisions)
                    Destroy(gameObject);
            }
        }

        private void OnBecameInvisible()
        {
           Destroy(gameObject);
        }
    }
}