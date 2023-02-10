using Components;
using DG.Tweening;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private Health _health;
        [SerializeField] private SpriteRenderer _sprite;
        [SerializeField] private GameObject _crystal;

        private Tween _tweenColor;

        private void OnEnable()
        {
            _health.Died += OnDied;
            _health.DamageTaken += OnTakeDamage;
        }

        private void OnDisable()
        {
            _health.Died -= OnDied;
            _health.DamageTaken -= OnTakeDamage;
        }

        private void OnDied()
        {
            _tweenColor.Kill();
            Instantiate(_crystal, transform.position, transform.rotation);
            Destroy(gameObject);
        }

        public void Initialize(Transform player)
        {
            _mover.Initialize(player);
        }

        private void OnTakeDamage()
        {
            if (_sprite.color != Color.white)
                _sprite.color = Color.white;
            
            _tweenColor = _sprite.DOColor(Color.red, 0.1f).OnComplete(() =>
            {
                _sprite.DOColor(Color.white, 0.1f);
            });
        }
    }
}