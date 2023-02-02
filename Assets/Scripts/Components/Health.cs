using System;
using Interfaces;
using UnityEngine;

namespace Components
{
    public class Health : MonoBehaviour, IDamagable
    {
        [SerializeField] private float _health;

        public event Action Died;
        public event Action DamageTaken;
        public float GetHealth => _health;

        public void GetDamage(float amout)
        {
            if (_health <= 0) return;
            
            _health = Mathf.Max(_health - amout, 0);

            DamageTaken?.Invoke();

            if (_health <= 0)
                Died.Invoke();
        }
    }
}