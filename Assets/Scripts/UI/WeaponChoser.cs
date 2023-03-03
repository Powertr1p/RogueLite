using System.Collections.Generic;
using Storages;
using UnityEngine;

namespace PowerTrip
{
    public class WeaponChoser : MonoBehaviour
    {
        [SerializeField] private ExperienceGainer _experience;
        [SerializeField] private PlayerWeaponInventory _invenotry;
        [SerializeField] private LevelWeaponStorage _storage;
        [SerializeField] private Transform _container;
        [SerializeField] private WeaponChoserSlot _slotPrefab;

        private List<WeaponChoserSlot> _slots = new();

        private readonly int _maxSlots = 3;

        private void OnEnable()
        {
            _experience.LevelUp += Show;
        }

        private void OnDisable()
        {
            _experience.LevelUp -= Show;

            for (int i = 0; i < _slots.Count; i++)
                _slots[i].ButtonClicked -= GiveWeapon;
        }

        private void Awake()
        {
            for (int i = 0; i < _maxSlots; i++)
            {
                var instance = Instantiate(_slotPrefab, _container);
                _slots.Add(instance);
                
                instance.ButtonClicked += GiveWeapon;
            }
            
            _container.gameObject.SetActive(false);
        }

        private void Show()
        {
            Time.timeScale = 0;
            
            SetWeaponData();
            
            _container.gameObject.SetActive(true);
        }

        private void SetWeaponData()
        {
            var dataToSet = _storage.GetThreeWeapons();

            for (int i = 0; i < _slots.Count; i++)
            {
                _slots[i].SetWeaponData(dataToSet[i]);
            }
        }

        private void GiveWeapon(Weapon weapon)
        {
            _invenotry.AddWeapon(weapon);
            
            CloseWindow();
        }

        private void CloseWindow()
        {
            ResetSlotsWeaponData();

            _container.gameObject.SetActive(false);
            Time.timeScale = 1;
        }

        private void ResetSlotsWeaponData()
        {
            for (int i = 0; i < _slots.Count; i++)
                _slots[i].ResetWeaponData();
        }
    }
}