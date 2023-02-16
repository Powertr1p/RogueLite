using UnityEngine;

namespace PowerTrip
{
    public class EnemyDamager : MonoBehaviour
    {
        #region Fields
        [SerializeField] private float _damage = 1f;
        [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private float _attackInterval = 1f;

        private float _distance = 0f;
        private float _nextAttackTimestamp = 0f;

        private Transform _t;
        private Player _player;
        #endregion

        private void Awake()
        {
            _t = transform;
        }

        public void Initialize(Player player)
        {
            _player = player;
        }

        private void Update()
        {
            if (_player is null) return;

            _distance = Vector3.Distance(_t.position, _player.transform.position);

            if (_distance > _attackDistance) return;

            PerformAttack();
        }

        private void PerformAttack()
        {
            if (_player is null) return;

            if (Time.time < _nextAttackTimestamp) return;

            if (_player.TryGetComponent(out IDamageable damageable))
            {
                damageable.GetDamage(_damage);

                _nextAttackTimestamp = Time.time + _attackInterval;
            }
        }
    }
}