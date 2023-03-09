using System.Collections.Generic;
using PowerTrip;
using UnityEngine;
using Weapons.CircleFloatingWeapon;

public class CircleFloatingWeapon : Weapon
{
    private List<Projectile> _activeProjectiles;
    
    protected override void Shoot()
    {
        var instance = Instantiate(Projectile, transform.position, Quaternion.identity);
        InitializeProjectile(instance);
    }

    protected override void InitializeProjectile(Projectile instance)
    {
        if (instance.TryGetComponent(out CircleFloatingProjectile projectile))
            projectile.SetPlayerTransform(transform);

        instance.Init(Vector3.forward, ProjectileSpeed, Damage);
    }
}
