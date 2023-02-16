using UnityEngine;

namespace PowerTrip
{
    [RequireComponent(typeof(CooldownTimer))]
    public abstract class Weapon : MonoBehaviour
    {
        [Header("Prefab")] 
        [SerializeField] protected Projectile Projectile;

        [Header("Weapon parameters")]
        [SerializeField] protected int Damage;
        [SerializeField] protected float ProjectileSpeed = 10f;

        private CooldownTimer _cooldownTimer;

        private void Awake()
        {
            _cooldownTimer = GetComponent<CooldownTimer>();
        }

        private void OnEnable()
        {
            _cooldownTimer.OnCooldownExpired += Shoot;
        }

        private void OnDisable()
        {
            _cooldownTimer.OnCooldownExpired -= Shoot;
        }

        protected abstract void Shoot();
        protected abstract void InitializeProjectile(Projectile instance);
    }
}