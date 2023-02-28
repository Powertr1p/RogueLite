using Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip
{
    public class ExperienceGainer : MonoBehaviour
    {
        [SerializeField] private Pickup _pickup;
        [SerializeField] private ExperienceTable _table;
        [SerializeField] private Image _uiImage;
        [SerializeField] private TextMeshProUGUI _levelText;

        private float _currentExp = 0f;
        private int _currentLevel = 1;
        private float _targetExpToLevelUp;
        private ExperienceData _currentData;
        
        private bool _isEnabled = false;
        
        private void OnEnable()
        {
            _pickup.OnConsume += TryAddExperience;
        }

        private void OnDisable()
        {
            _pickup.OnConsume -= TryAddExperience;
        }

        private void Start()
        {
            _currentData = GetCurrentExperienceData();
            
            UpdateVisuals();
        }

        public void SetState(bool state)
        {
            _isEnabled = state;
        }
        
        private void TryAddExperience(ICollectable collectable)
        {
            if (_isEnabled is false) return;
            
            if (collectable.Transform.TryGetComponent(out ExpCrystal crystal))
                AddExperience(crystal.Experience);
        }

        private void AddExperience(float amount)
        {
            _currentExp += amount;
            
            UpdateVisuals();
        }

        private void UpdateVisuals()
        {
            var currentValue = Calculate();
            
            _uiImage.fillAmount = currentValue;

            if (_currentExp >= _currentData.Experience)
            {
                _currentExp = 0;
                _currentLevel++;
                _currentData = GetCurrentExperienceData();
                _uiImage.fillAmount = 0;
            }

            _levelText.text = _currentLevel.ToString();
        }

        private float Calculate()
        {
            return _currentExp / _currentData.Experience;
        }

        private ExperienceData GetCurrentExperienceData()
        {
            return _table.Datas[_currentLevel - 1];
        }
    }
}