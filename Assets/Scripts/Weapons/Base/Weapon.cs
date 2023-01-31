using UnityEngine;

namespace Weapons.Base
{
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Prefab")] 
        [SerializeField] protected Projectile Projectile;

        [Header("Weapon parameters")]
        [SerializeField] protected float Cooldown = 1f;
        [SerializeField] protected int Damage;
        [SerializeField] protected float ProjectileSpeed = 10f;

        private float _timeUntilNextShot;

        private void Start()
        {
            _timeUntilNextShot = Time.time + Cooldown;
        }

        private void Update()
        {
            if (IsCooldownEnded())
            {
                Shoot();
                SetupNewCooldownTimer();
            }
        }

        protected abstract void Shoot();
        protected abstract void InitializeProjectile(Projectile instance);

        private bool IsCooldownEnded()
        {
            return Time.time >= _timeUntilNextShot;
        }

        private void SetupNewCooldownTimer()
        {
            _timeUntilNextShot = Time.time + Cooldown;
        }
    }
}