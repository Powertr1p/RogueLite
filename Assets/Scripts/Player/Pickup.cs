using System;
using System.Collections.Generic;
using UnityEngine;

namespace PowerTrip
{
    [RequireComponent(typeof(Detector))]
    public class Pickup : MonoBehaviour
    {
        [Tooltip("Скорость с которой лут летит в сторону игрока")]
        [SerializeField] private float _pickupSpeed;

        public Action<ICollectable> OnConsume;
        
        private Transform _transform;
        private Detector _detector;
        private float _timeSinceLastUpdate;
        private List<ICollectable> _detectedLoot = new List<ICollectable>();

        private bool _isEnabled = false;

        private void Awake()
        {
            _transform = transform;
            _detector = GetComponent<Detector>();
        }

        public void SetState(bool state)
        {
            _isEnabled = state;
        }

        public void UpdatePickup()
        {
            if (_isEnabled is false) return;

            Detect();
            MoveDetectedLootTowardsPlayer();
        }
        
        private void Detect()
        {
            var detected = _detector.Detect();

            for (int i = 0; i < _detector.Hits; i++)
            {
                if (detected[i].TryGetComponent(out ICollectable collectable))
                {
                    if (_detectedLoot.Contains(collectable)) continue;
                    
                    collectable.Collect(GetDirection(collectable), RegisterCollectible);
                }
            }
        }

        private void RegisterCollectible(ICollectable collectable)
        {
            _detectedLoot.Add(collectable);
        }

        private void MoveDetectedLootTowardsPlayer()
        {
            for (int i = 0; i < _detectedLoot.Count; i++)
            {
                Vector2 direction = GetDirection(_detectedLoot[i]);
                _detectedLoot[i].Transform.position += (Vector3) (direction * (_pickupSpeed * Time.deltaTime));

                if (CanConsume(_detectedLoot[i]))
                    Consume(_detectedLoot[i]);
            }
        }

        private Vector2 GetDirection(ICollectable collectable)
        {
            return (_transform.position - collectable.Transform.position).normalized;
        }

        private bool CanConsume(ICollectable collectable)
        {
            return Vector3.Distance(_transform.position, collectable.Transform.position) < 0.1f;
        }

        private void Consume(ICollectable collectable)
        {
            OnConsume?.Invoke(collectable);

            collectable.Consume();
            _detectedLoot.Remove(collectable);
        }
    }
}