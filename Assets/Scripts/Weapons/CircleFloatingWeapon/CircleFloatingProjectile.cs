using System;
using PowerTrip;
using UnityEngine;

namespace Weapons.CircleFloatingWeapon
{
    public class CircleFloatingProjectile : Projectile
    {
        public float radius = 1f;
        private float angle; 
        
        private Transform _playerTransform;

        public override void Init(Vector3 direction, float speed, float damage)
        {
            Speed = speed;
            Damage = damage;
        }

        public void SetPlayerTransform(Transform player)
        {
            _playerTransform = player;
        }

        protected override void  Update()
        {
            angle += Speed * Time.deltaTime;

            // calculate the position of the projectile based on the angle and radius
            float x = _playerTransform.position.x + Mathf.Cos(angle) * radius;
            float y = _playerTransform.position.y + Mathf.Sin(angle) * radius;
            transform.position = new Vector2(x, y);
        }
    }
}