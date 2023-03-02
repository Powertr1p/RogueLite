using System;
using System.Collections.Generic;
using PowerTrip.WeaponChoser;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip
{
    public class UI_WeaponChoser : MonoBehaviour
    {
        [SerializeField] private ExperienceGainer _experience;
        [SerializeField] private Transform _container;
        [SerializeField] private WeaponChoserSlot _slotPrefab;

        private List<WeaponChoserSlot> _slots = new();

        private readonly int _maxSlots = 3;
        
        //TODO: сделать контроллер баттонов
        //TODO: создавать их
        //TODO: у каждой кнопки свой обработчик со своим ивентом
        //TODO: отдавать в ивенте дату баттона с оружием
        
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
            _container.gameObject.SetActive(true);
        }

        private void GiveWeapon()
        {
            CloseWindow();
        }

        private void CloseWindow()
        {
            _container.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}