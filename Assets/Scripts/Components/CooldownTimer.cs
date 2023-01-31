using System;
using UnityEngine;

namespace Components
{
    public class CooldownTimer : MonoBehaviour
    {
        [SerializeField] private float _cooldown = 1f;

        public event Action CooldownExpired;
        
        private float _cooldownExpireTime;

        private void Start()
        {
            _cooldownExpireTime = Time.time + _cooldown;
        }
        
        private void Update()
        {
            if (IsCooldownEnded())
            {
                CooldownExpired?.Invoke();
                SetupNewCooldownTimer();
            }
        }
        
        private bool IsCooldownEnded()
        {
            return Time.time >= _cooldownExpireTime;
        }

        private void SetupNewCooldownTimer()
        {
            _cooldownExpireTime = Time.time + _cooldown;
        }
    }
}