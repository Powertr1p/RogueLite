using System;
using UnityEngine;

namespace PowerTrip
{
    public class RoundWeapon : Weapon
    {
        #region Fields
        [SerializeField] private float _delay = 0.1f;
        [SerializeField] private float _numberOfSpawns = 10f;
        [SerializeField] private float _radius = 5f;

        private Vector3 _direction;
        #endregion

        protected override void InitializeProjectile(Projectile instance)
        {
            instance.Init(_direction, ProjectileSpeed, Damage);
        }

        protected override void Shoot()
        {
            float nextAngle = 2 * MathF.PI / _numberOfSpawns;
            float angle = 0;

            for (int i = 0; i < _numberOfSpawns; i++)
            {
                float x = Mathf.Cos(angle) * _radius;
                float y = Mathf.Sin(angle) * _radius;

                var instance = Instantiate(Projectile, transform.position, Quaternion.identity);

                _direction = new Vector3(x, y);
                
                InitializeProjectile(instance);

                angle += nextAngle;
            }
        }
    }
}
