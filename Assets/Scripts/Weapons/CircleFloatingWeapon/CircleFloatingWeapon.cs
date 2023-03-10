using System.Collections.Generic;
using PowerTrip;
using UnityEngine;
using Weapons.CircleFloatingWeapon;

public class CircleFloatingWeapon : Weapon
{
    [SerializeField] private float _radius = 2f;
    [SerializeField] private int _activeLimit = 5;

    private List<Projectile> _activeProjectiles = new List<Projectile>();
    
    protected override void Shoot()
    {
        var instance = Instantiate(Projectile, transform.position, Quaternion.identity);
        
        _activeProjectiles.Add(instance);

        instance.Destoryed += RemoveActiveProjectile;
        
        if (instance.TryGetComponent(out CircleFloatingProjectile projectile))
            projectile.SetDependecies(transform, _radius);
        
        InitializeProjectile(instance);
    }

    protected override void InitializeProjectile(Projectile instance)
    {
        instance.Init(Vector3.forward, ProjectileSpeed, Damage);
    }

    private void RemoveActiveProjectile(Projectile projectile)
    {
        _activeProjectiles.Remove(projectile);
        
        projectile.Destoryed -= RemoveActiveProjectile;
    }
}
