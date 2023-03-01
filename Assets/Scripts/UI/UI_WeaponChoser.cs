using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip
{
    public class UI_WeaponChoser : MonoBehaviour
    {
        [SerializeField] private ExperienceGainer _experience;

        [SerializeField] private Button _buttonLeft;
        [SerializeField] private Button _buttonCenter;
        [SerializeField] private Button _buttonRight;
        [SerializeField] private GameObject _container;
        
        //TODO: сделать контроллер баттонов
        //TODO: создавать их
        //TODO: у каждой кнопки свой обработчик со своим ивентом
        //TODO: отдавать в ивенте дату баттона с оружием
        
        private void OnEnable()
        {
            _buttonLeft.onClick.AddListener(OnLeftButtonClick);
            _buttonCenter.onClick.AddListener(OnCenterButtonClick);
            _buttonRight.onClick.AddListener(OnRightButtonClick);

            _experience.LevelUp += Show;
        }

        private void OnDisable()
        {
            _buttonLeft.onClick.RemoveListener(OnLeftButtonClick);
            _buttonRight.onClick.RemoveListener(OnCenterButtonClick);
            _buttonCenter.onClick.RemoveListener(OnRightButtonClick);
            
            _experience.LevelUp -= Show;
        }

        private void Show()
        {
            Time.timeScale = 0;
            _container.gameObject.SetActive(true);
        }

        private void OnLeftButtonClick()
        {
            Debug.Log("clicked 1");
            CloseWindow();
        }

        private void OnCenterButtonClick()
        {
            Debug.Log("clicked 2");
            CloseWindow();
        }
        
        private void OnRightButtonClick()
        {
            Debug.Log("clicked 3");
            CloseWindow();
        }

        private void CloseWindow()
        {
            _container.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }
}