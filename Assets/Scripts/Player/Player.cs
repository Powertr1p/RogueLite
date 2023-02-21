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

        private bool _isEnabled = false;
        #endregion

        private void Awake()
        {
            if (_health != null)
            {
                _health.OnDamageTaken += HandleDamageTaken;
                _health.OnDeath += HandleDeath;
            }
        }

        private void Start()
        {
            // TODO: use some kind of boostrapped class to switch player's state

            SetInputState(true);
            SetMovementState(true);
            SetPickupState(true);

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
        }

        private bool IsNotNull(MonoBehaviour reference)
        {
            return !ReferenceEquals(reference, null);
        }

        private void OnDestroy()
        {
            if (_health != null)
            {
                _health.OnDamageTaken -= HandleDamageTaken;
                _health.OnDeath -= HandleDeath;
            }
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
        }
    }
}