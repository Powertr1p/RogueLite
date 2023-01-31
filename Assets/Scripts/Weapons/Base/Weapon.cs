using System;
using Components;
using UnityEngine;

namespace Weapons.Base
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
            _cooldownTimer.CooldownExpired += Shoot;
        }

        private void OnDisable()
        {
            _cooldownTimer.CooldownExpired -= Shoot;
        }

        protected abstract void Shoot();
        protected abstract void InitializeProjectile(Projectile instance);
    }
}