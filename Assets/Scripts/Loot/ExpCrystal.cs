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
        private Collider2D _collider;
        private bool _canCollect = true;
        
        private void Awake()
        {
            _collider = GetComponent<Collider2D>();
        }

        public override void Collect(Vector2 direction, Action<ICollectable> onComplete)
        {
            if (!_canCollect) return;
            _canCollect = false;
            
            var newDir = -(Vector3)direction + transform.position;

            //ToggleCollider(false);
            
            transform.DOMove(newDir, 0.25f).SetEase(Ease.InQuad).OnComplete(() =>
            {
               //ToggleCollider(true);
                onComplete?.Invoke(this);
            });
        }

        public override void Consume()
        {
            Destroy(gameObject);
        }

        private void ToggleCollider(bool isActive)
        {
            _collider.enabled = isActive;
        }
    }
}