using System;
using UnityEngine;

namespace PowerTrip
{
    public class ExperienceGainer : MonoBehaviour
    {
        [SerializeField] private float _currentExp = 0f;
        [SerializeField] private Pickup _pickup;

        private bool _isEnabled = false;
        
        private void OnEnable()
        {
            _pickup.OnConsume += TryAddExperience;
        }

        private void OnDisable()
        {
            _pickup.OnConsume -= TryAddExperience;
        }

        public void SetState(bool state)
        {
            _isEnabled = state;
        }
        
        private void TryAddExperience(ICollectable collectable)
        {
            if (_isEnabled is false) return;
            
            if (collectable.Transform.TryGetComponent(out ExpCrystal crystal))
                AddExperience(crystal.Experience);
        }

        private void AddExperience(float amount)
        {
            _currentExp += amount;
        }
    }
}