using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip
{
    public class WeaponChoserSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _title;

        private Weapon _currentWeapon;
        
        public event Action<Weapon> ButtonClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        public void SetWeaponData(Weapon weapon)
        {
            _currentWeapon = weapon;
            _title.text = weapon.gameObject.name;
        }

        public void ResetWeaponData()
        {
            _currentWeapon = null;
        }

        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke(_currentWeapon);
        }
    }
}