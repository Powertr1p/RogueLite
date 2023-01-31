using System;
using Interfaces;
using UnityEngine;

namespace Enemy
{
    public class EnemyMover : MonoBehaviour
    {
        [SerializeField] private float _speed = 2f;
        
        private Transform _target;
        private Transform _transform;

        private bool _isInitialized;

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            if (!_isInitialized) return;
            
            _transform.position =  Vector3.MoveTowards(transform.position, _target.position, _speed * Time.deltaTime);
        }

        public void Initialize(Transform target)
        {
            _target = target;
            _isInitialized = true;
        }
    }
}