using System;
using UnityEngine;
using UnityEngine.UI;

using DG.Tweening;
using TMPro;

namespace PowerTrip
{
    public class UI_TextLabel : MonoBehaviour
    {
        #region Fields
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Color _color;

        private Tween _tweenMovement;
        private Tween _tweenFade;
        #endregion

        public UI_TextLabel SetPosition(Vector3 position)
        {
            transform.position = position;

            return this;
        }

        public UI_TextLabel SetText(string text)
        {
            _text.text = text;

            return this;
        }

        public void PlayTween(Vector3 endValue, float duration, Ease ease = Ease.Linear, Action callback = null)
        {
            // Movement
            // ————————————————————

            _tweenMovement?.Kill(true);
            _tweenMovement = transform.DOMove
            (
                endValue: endValue,
                duration: duration
            )
            .SetEase(ease);

            // Fade
            // ————————————————————

            _text.color = _color;

            _tweenFade?.Kill();
            _tweenFade = _text.DOFade
            (
                endValue: 0f,
                duration: duration
            )
            .SetEase(ease)
            .OnComplete(() =>
            {
                callback?.Invoke();
            });
        }
    }
}