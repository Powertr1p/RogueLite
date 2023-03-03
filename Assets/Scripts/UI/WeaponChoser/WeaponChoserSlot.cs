using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip.WeaponChoser
{
    public class WeaponChoserSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;
        [SerializeField] private TextMeshProUGUI _title;

        public event Action ButtonClicked;

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
            _title.text = weapon.gameObject.name;
        }

        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke();
        }
    }
}