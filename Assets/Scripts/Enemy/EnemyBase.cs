using System;
using Components;
using UnityEngine;

namespace Enemy
{
    [RequireComponent(typeof(Health))]
    [RequireComponent(typeof(EnemyMover))]
    public class EnemyBase : MonoBehaviour
    {
        [SerializeField] private EnemyMover _mover;
        [SerializeField] private Health _health;

        private void OnEnable()
        {
            _health.Died += OnDied;
        }

        private void OnDisable()
        {
            _health.Died -= OnDied;
        }

        private void OnDied()
        {
            Destroy(gameObject);
        }

        public void Initialize(Transform player)
        {
            _mover.Initialize(player);
        }
    }
}