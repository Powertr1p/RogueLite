using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace
{
    [RequireComponent(typeof(SpriteRenderer))]
    public class BloodAnimation : MonoBehaviour
    {
        private SpriteRenderer _renderer;
        private Color _defaultColor;
        private Transform _transform;
        private GameObject _gameObject;
        
        public void Initialize()
        {
            _renderer = GetComponent<SpriteRenderer>();
            
            _defaultColor = _renderer.color;
            _transform = transform;
            _gameObject = gameObject;
        }

        public void Activate()
        {
            _gameObject.SetActive(true);
            _renderer.color = _defaultColor;
            
            SetRandomRotation();
            
            var targetValue = Random.Range(0.5f, 0.8f);
            
            _transform.DOScale(new Vector3(targetValue, targetValue, targetValue), 0.15f).SetEase(Ease.InOutQuad)
                .OnComplete(() =>
                {
                    _gameObject.isStatic = true;
                });
        }

        public void Deactivation(Action onComplete)
        {
            _renderer.DOFade(0f, 0.1f).OnComplete(() =>
            {
                _transform.localScale = Vector3.zero;
                _transform.gameObject.SetActive(false);
                _gameObject.isStatic = false;
                
                onComplete?.Invoke();
            });
        }

        private void SetRandomRotation()
        {
            var euler = _transform.eulerAngles;
            euler.z = Random. Range(0.0f, 360.0f);
            _transform.eulerAngles = euler;
        }
    }
}