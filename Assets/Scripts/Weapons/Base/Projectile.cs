using System;
using UnityEngine;

namespace PowerTrip
{
    public class Projectile : MonoBehaviour, IDamaging
    {
        [SerializeField] private Transform _visual;
        [SerializeField] private bool _hasHitEffect = false;
        [SerializeField] private GameObject _deathEffect;

        public event Action<Projectile> Destoryed;
        
        protected Vector3 Direction;
        protected float Speed;
        protected float Damage;

        private int _maxNumberOfCollisions = 1;
        private int _currentNumberOfCollisions;
        private float _destroyTimer = 0;

        protected bool IsInitialized;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out IDamageable damageable))
                DealDamage(damageable, Damage);
        }

        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }

        protected virtual void Update()
        {
            if (!IsInitialized) return;
            
            transform.Translate(Direction * (Speed * Time.deltaTime));
        }

        protected void OnDestroy()
        {
            Destoryed?.Invoke(this);
        }

        public virtual void Init(Vector3 direction, float speed, float damage)
        {
            Direction = direction;
            Speed = speed;
            Damage = damage;

            RotateToDirection();
            
            IsInitialized = true;
            Destroy(gameObject, 5f);
        }

        public void DealDamage(IDamageable damageable, float amout)
        {
            if (_currentNumberOfCollisions >= _maxNumberOfCollisions) return;

            _currentNumberOfCollisions++;

            damageable.GetDamage(Damage);

            if (_currentNumberOfCollisions >= _maxNumberOfCollisions)
            {
                // TODO: use pooling
                PlayHitEffect();
                Destroy(gameObject, _destroyTimer);
                
                IsInitialized = false;
            }
        }

        private void RotateToDirection()
        {
            if (Direction != Vector3.zero)
            {
                float angle = Mathf.Atan2(Direction.y, Direction.x) * Mathf.Rad2Deg;
                _visual.localRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            }
        }

        private void PlayHitEffect()
        {
            if (!_hasHitEffect) return;
            
            _visual.gameObject.SetActive(false);
            _deathEffect.SetActive(true);
            _destroyTimer = _deathEffect.GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length;
        }
    }
}