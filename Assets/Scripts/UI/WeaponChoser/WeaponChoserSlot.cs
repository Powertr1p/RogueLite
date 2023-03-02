using System;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip.WeaponChoser
{
    public class WeaponChoserSlot : MonoBehaviour
    {
        [SerializeField] private Button _button;

        public event Action ButtonClicked;

        private void OnEnable()
        {
            _button.onClick.AddListener(OnButtonClicked);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(OnButtonClicked);
        }

        private void OnButtonClicked()
        {
            ButtonClicked?.Invoke();
        }
    }
}