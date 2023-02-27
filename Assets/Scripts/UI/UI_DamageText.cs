using System.Globalization;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace PowerTrip
{
    public class UI_DamageText : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private Text _textPrefab;
        
        [Header("Animation")]
        [SerializeField] private float _moveOffset = 5f;
        [SerializeField] private float _moveOffsetDuration = 0.5f;
        [SerializeField] private Ease _ease = Ease.Linear;
        
        public void ShowDamageText(float amount, Vector3 position)
        {
            var instance = Instantiate(_textPrefab, transform);
            instance.transform.position = GetScreenPosition(position);
            instance.text = amount.ToString(CultureInfo.InvariantCulture);

            instance.transform.DOMoveY(GetScreenPosition(position).y + _moveOffset, 0.5f).SetEase(_ease);
            Destroy(instance.gameObject, 0.6f);
        }
        
        private Vector3 GetScreenPosition(Vector3 worldPosition)
        {
            return _camera.WorldToScreenPoint(worldPosition);
        }
    }
}