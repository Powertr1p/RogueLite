using System;
using UnityEngine;

namespace PowerTrip
{
    public class Player : MonoBehaviour
    {
        #region Fields
        [SerializeField] private PlayerInput _input;
        [SerializeField] private PlayerMovement _movement;
        [SerializeField] private Pickup _pickup;
        [SerializeField] private PlayerWeaponInventory _weaponInventory;
        [SerializeField] private Health _health;
        [SerializeField] private ExperienceGainer _experience;
        [SerializeField] private WeaponPickuper _weaponPickuper;

        private bool _isEnabled = false;
        #endregion

        private void OnEnable()
        {
            if (IsNotNull(_health))
            {
                _health.OnDamageTaken += HandleDamageTaken;
                _health.OnDeath += HandleDeath;
            }
        }

        private void OnDisable()
        {
            if (IsNotNull(_health))
            {
                _health.OnDamageTaken -= HandleDamageTaken;
                _health.OnDeath -= HandleDeath;
            }
        }

        private void Start()
        {
            // TODO: use some kind of boostrapped class to switch player's state

            SetInputState(true);
            SetMovementState(true);
            SetPickupState(true);
            SetExperienceGainerState(true);
            SetWeaponPickupState(true);

            SetState(true);
        }

        private void Update()
        {
            if (_isEnabled is false) return;

            if (IsNotNull(_movement))
            {
                _movement.UpdateMovement(_input.Direction);
            }

            if (IsNotNull(_pickup))
            {
                _pickup.UpdatePickup();
            }

            if (IsNotNull(_weaponPickuper))
            {
                _weaponPickuper.UpdateWeaponPickup();
            }
        }

        private bool IsNotNull(MonoBehaviour reference)
        {
            return !ReferenceEquals(reference, null);
        }

        private void OnDestroy()
        {
            OnDisable();
        }

        public Player SetState(bool state)
        {
            _isEnabled = state;

            return this;
        }

        public Player SetInputState(bool state)
        {
            if (_input is null) return this;

            _input.SetState(state);

            return this;
        }

        public Player SetWeaponPickupState(bool state)
        {
            if (_weaponPickuper is null) return this;
            
            _weaponPickuper.SetState(state);

            return this;
        }

        public Player SetExperienceGainerState(bool state)
        {
            if (_experience is null) return this;

            _experience.SetState(state);

            return this;
        }

        public Player SetMovementState(bool state)
        {
            if (_movement is null) return this;

            _movement.SetState(state);

            return this;
        }

        public Player SetPickupState(bool state)
        {
            if (_pickup is null) return this;

            _pickup.SetState(state);

            return this;
        }

        private void HandleDamageTaken()
        {
            // ...
        }

        private void HandleDeath()
        {
            Debug.Log($"Player is dead, shame on you.");

            SetState(false);

            SetInputState(false);
            SetMovementState(false);
            SetPickupState(false);
            SetExperienceGainerState(false);
            SetWeaponPickupState(false);
        }
    }
}