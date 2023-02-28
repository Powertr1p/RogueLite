using PowerTrip.PickableWeapons;
using UnityEngine;

namespace PowerTrip
{
    [RequireComponent(typeof(Detector))]
    public class WeaponPickuper : MonoBehaviour
    {
        [SerializeField] private PlayerWeaponInventory _weaponInventory;
        
        private Detector _weaponDetector;
        private Pickup _weaponPickuper;

        private bool _isEnabled = false;

        private void Awake()
        {
            _weaponDetector = GetComponent<Detector>();
            _weaponPickuper = GetComponent<Pickup>();
        }

        private void OnEnable()
        {
            _weaponPickuper.OnConsume += SetNewWeapon;
        }

        private void OnDisable()
        {
            _weaponPickuper.OnConsume -= SetNewWeapon;
        }

        public void SetState(bool state)
        {
            _isEnabled = state;
            _weaponPickuper.SetState(state);
        }

        public void UpdateWeaponPickup()
        {
            _weaponPickuper.UpdatePickup();
        }

        private void SetNewWeapon(ICollectable collectable)
        {
            if (collectable.Transform.TryGetComponent(out PickableWeapon weapon))
            {
                _weaponInventory.AddWeapon(weapon.GetWeaponPrefab);
            }
        }
    }
}