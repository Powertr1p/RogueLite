using UnityEngine;
using Weapons.Base;

namespace Weapons.SimpleWeapon1
{
    public class SimpleWeapon : Weapon
    {
        [SerializeField] private ShootingPivotRotator _shootingPivot;

        private Vector3 _projectileDirection => _shootingPivot.Direction;
        
        protected override void Shoot()
        {
            var instance = Instantiate(Projectile, transform.position, Quaternion.identity);
            InitializeProjectile(instance);
        }

        protected override void InitializeProjectile(Projectile instance)
        {
            instance.Init(_projectileDirection, ProjectileSpeed, Damage);
        }
    }
}