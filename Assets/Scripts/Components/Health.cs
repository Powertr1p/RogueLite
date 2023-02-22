using System;
using UnityEngine;

namespace PowerTrip
{
    public sealed class Health : MonoBehaviour, IDamageable
    {
        #region Events
        public event Action OnDamageTaken;
        public event Action OnDeath;
        #endregion

        #region Fields
        [SerializeField] private UI_Healthbar _ui;

        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _postHitInvincibilityTime = 0f;

        private float _currentHealth;
        private float _postHitInvincibilityTimestamp = 0f;
        #endregion

        #region Accessors
        public float MaxHealth { get => _maxHealth; }
        public float CurrentHealth { get => _currentHealth; }
        #endregion

        private void Start()
        {
            Reset();
        }

        private void Reset()
        {
            _currentHealth = _maxHealth;
        }

        public void GetDamage(float amout)
        {
            if (_currentHealth <= 0) return;

            if (Time.time < _postHitInvincibilityTimestamp) return;

            _currentHealth = Mathf.Max(_currentHealth - amout, 0);

            if (_postHitInvincibilityTime > 0f)
            {
                _postHitInvincibilityTimestamp = Time.time + _postHitInvincibilityTime;
            }

            OnDamageTaken?.Invoke();

            if (_ui != null)
            {
                _ui.UpdateHealthbar(_currentHealth / _maxHealth);
            }

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }
    }
}