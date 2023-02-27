using System;
using UnityEngine;

using NaughtyAttributes;

namespace PowerTrip
{
    public sealed class Health : MonoBehaviour, IDamageable
    {
        #region Events
        public event Action OnDamageTaken;
        public event Action OnDeath;
        #endregion

        #region Fields
        [Header("Component Links:")]
        [SerializeField] private UI_Healthbar _healthbar;

        [Header("Settings:")]
        [SerializeField] private float _maxHealth = 100;
        [SerializeField] private float _postHitInvincibilityTime = 0f;

        [SerializeField] private bool _isShowingDamageText = true;
        [SerializeField] [ShowIf("_isShowingDamageText")] private float _damageTextOffset = 0f;

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

        public void GetDamage(float value)
        {
            if (_currentHealth <= 0) return;

            if (Time.time < _postHitInvincibilityTimestamp) return;

            _currentHealth = Mathf.Max(_currentHealth - value, 0);

            if (_postHitInvincibilityTime > 0f)
            {
                _postHitInvincibilityTimestamp = Time.time + _postHitInvincibilityTime;
            }

            OnDamageTaken?.Invoke();

            if (_currentHealth <= 0)
            {
                OnDeath?.Invoke();
            }

            // UI
            // ————————————————————

            // Updating healthbar
            if (_healthbar != null)
            {
                _healthbar.UpdateHealthbar(_currentHealth / _maxHealth);
            }

            // Showing damage text
            if (_isShowingDamageText is true)
            {
                Vector3 damageTextPosition = transform.position;
                damageTextPosition.y += _damageTextOffset;

                UIRouter.ShowDamageText(damageTextPosition, value);
            }
        }
    }
}