using UnityEngine;
using UnityEngine.UI;

using NaughtyAttributes;

using DG.Tweening;

namespace PowerTrip
{
    public class UI_Healthbar : MonoBehaviour
    {
        #region Fields
        [Header("Component Links:")]
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _sprFill;

        [Header("Settings:")]
        [SerializeField] private Ease _easeFill;
        [SerializeField] private float _tweenFillDuration = 0.25f;

        [SerializeField] private bool _isDynamic = true;

        [SerializeField] [ShowIf("_isDynamic")] private Ease _easeFade;
        [SerializeField] [ShowIf("_isDynamic")] private float _tweenFadeDuration = 1f;
        [SerializeField] [ShowIf("_isDynamic")] private float _tweenFadeOutDelay = 1f;

        private Tween _tweenAlpha;
        #endregion

        private void Start()
        {
            if (_isDynamic is false)
            {
                Show(immediate: true);
            }

            else
            {
                Hide(immediate: true);
            }
        }

        public void Show(bool immediate = false)
        {
            if (immediate is true)
            {
                _canvasGroup.alpha = 1f;
                return;
            }

            _tweenAlpha?.Kill();
            _tweenAlpha = _canvasGroup.DOFade(1f, _tweenFadeDuration)
                                      .SetEase(_easeFade);
        }

        public void Hide(bool immediate = false, float delay = 0f)
        {
            if (immediate is true)
            {
                _canvasGroup.alpha = 0f;
                return;
            }

            _tweenAlpha?.Kill();
            _tweenAlpha = _canvasGroup.DOFade(0f, _tweenFadeDuration)
                                      .SetEase(_easeFade)
                                      .SetDelay(delay);
        }

        public void UpdateHealthbar(float targetValue)
        {
            if (_isDynamic is true)
            {
                Show();
            }

            _sprFill.DOFillAmount
            (
                endValue: targetValue,
                duration: _tweenFillDuration
            )
            .SetEase(_easeFill)
            .OnComplete(() =>
            {
                if (_isDynamic is true)
                {
                    Hide(delay: _tweenFadeOutDelay);
                }
            });
        }
    }
}