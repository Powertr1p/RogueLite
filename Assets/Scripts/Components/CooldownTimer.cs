using System;
using UnityEngine;

namespace PowerTrip
{
    public sealed class CooldownTimer : MonoBehaviour
    {
        #region Events
        public event Action OnCooldownExpired;
        #endregion

        #region Fields
        [SerializeField] private float _cooldown = 1f;

        private float _cooldownExpirationTimestamp = 0f;
        #endregion

        private void Start()
        {
            _cooldownExpirationTimestamp = Time.time + _cooldown;
        }
        
        private void Update()
        {
            if (IsCooldownEnded() is true)
            {
                OnCooldownExpired?.Invoke();

                SetupNewCooldownTimer();
            }
        }
        
        private bool IsCooldownEnded()
        {
            return Time.time >= _cooldownExpirationTimestamp;
        }

        private void SetupNewCooldownTimer()
        {
            _cooldownExpirationTimestamp = Time.time + _cooldown;
        }
    }
}