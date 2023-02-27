using UnityEngine;

using DG.Tweening;

namespace PowerTrip
{
    public class UI_DamageLabels : MonoBehaviour
    {
        [Header("Component Links:")]
        [SerializeField] private Camera _camera;
        [SerializeField] private UI_TextLabel _prefabTextLabel;
        
        [Header("Animation:")]
        [SerializeField] private Ease _labelMovementEase = Ease.Linear;

        [SerializeField] private float _labelMovementOffset = 5f;
        [SerializeField] private float _labelMovementDuration = 0.5f;

        private bool _isEnabled = true;

        public void SetState(bool state)
        {
            _isEnabled = state;
        }

        public void ShowDamageText(Vector3 position, float value)
        {
            if (_isEnabled is false) return;

            // TODO: use pooling

            var label = Instantiate(_prefabTextLabel, transform);

            Vector3 endValue = position;
            endValue.y += _labelMovementOffset;

            label.SetPosition(position)
                 .SetText($"{value}")
                 .PlayTween
                 (
                    endValue: endValue,
                    duration: _labelMovementDuration,
                    ease: _labelMovementEase,
                    callback: () =>
                    {
                        // TODO: use pooling

                        Destroy(label.gameObject, 0.125f);
                    }
                 );
        }
    }
}