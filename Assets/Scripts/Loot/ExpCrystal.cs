using System;
using DG.Tweening;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

namespace PowerTrip
{
    [RequireComponent(typeof(Collider))]
    public class ExpCrystal : LootBase
    {
        [SerializeField] private Ease _easing = Ease.InQuad;
        [SerializeField] private float _animationDuration = 0.25f;
        
        private bool _canCollect = true;

        public override void Collect(Vector2 direction, Action<ICollectable> onComplete)
        {
            if (!_canCollect) return;
            _canCollect = false;
            
            var newDir = -(Vector3)direction + transform.position;

            transform.DOMove(newDir, _animationDuration).SetEase(_easing).OnComplete(() =>
            {
                onComplete?.Invoke(this);
            });
        }

        public override void Consume()
        {
            Destroy(gameObject);
        }
    }
}