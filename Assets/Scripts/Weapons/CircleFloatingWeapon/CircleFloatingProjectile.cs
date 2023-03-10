using System;
using PowerTrip;
using UnityEngine;

namespace Weapons.CircleFloatingWeapon
{
    public class CircleFloatingProjectile : Projectile
    {
        private float _radius = 1f;
        private float _angle; 
        
        private Transform _playerTransform;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public override void Init(Vector3 direction, float speed, float damage)
        {
            Speed = speed;
            Damage = damage;
        }

        public void SetDependecies(Transform player, float radius)
        {
            _playerTransform = player;
            _radius = radius;

            IsInitialized = true;
        }

        protected override void Update()
        {
            if (!IsInitialized) return;
            
            _angle += Speed * Time.deltaTime;
            
            float x = _playerTransform.position.x + Mathf.Cos(_angle) * _radius;
            float y = _playerTransform.position.y + Mathf.Sin(_angle) * _radius;
            _transform.position = new Vector2(x, y);
        }
    }
}