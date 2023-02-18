using UnityEngine;

namespace PowerTrip
{
    public class AutoTargetWeapon : Weapon
    {
        #region Fields
        [SerializeField] private LayerMask _layerToDetectEnemies;

        private Transform _target;
        #endregion

        protected override void Shoot()
        {
            FindNearestTarget();
            
            if (!ReferenceEquals(_target, null))
            {
                var instance = Instantiate(Projectile, transform.position, Quaternion.identity);

                InitializeProjectile(instance);
            }
        }

        protected override void InitializeProjectile(Projectile instance)
        {
            var lastTarget = _target;

            var direction = (lastTarget.position - transform.position).normalized;

            instance.Init(direction, ProjectileSpeed, Damage);

            _target = null;
        }

        private void FindNearestTarget()
        {
            var hit = Physics2D.OverlapCircle(transform.position, 7f, _layerToDetectEnemies);

            if (!ReferenceEquals(hit, null))
            {
                _target = hit.transform;
            }
        }
    }
}
