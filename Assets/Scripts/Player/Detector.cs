using System;
using System.Collections.Generic;
using UnityEngine;

namespace PowerTrip
{
    [RequireComponent(typeof(Transform))]
    public class Detector : MonoBehaviour
    {
        [SerializeField] private float _radius;
        [SerializeField] private LayerMask _layer;
        [SerializeField] private Color _radiusColor;

        public int Hits => _hits;
        
        private const float DelayBetweenChecks = 1f;
        private float _timeSinceLastUpdate;
        private int _hits;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public Collider2D[] Detect()
        {
            Collider2D[] results = new Collider2D[10];
            _hits = Physics2D.OverlapCircleNonAlloc(_transform.position, _radius, results, _layer);

            return _hits > 0 ? results : new Collider2D[0];
        }

        //TODO: enable if lags
        private bool CanCheck()
        {
            _timeSinceLastUpdate += Time.deltaTime;
            
            if (_timeSinceLastUpdate < DelayBetweenChecks) return false;
            
            _timeSinceLastUpdate = 0f;

            return true;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = _radiusColor;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}