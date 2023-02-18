using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace PowerTrip
{
    [RequireComponent(typeof(Detector))]
    public class Pickup : MonoBehaviour
    {
        //[Tooltip("Радиус, в котором игрок может поднимать предметы")]
        // [Range(0f, 100f)]
        // [SerializeField] private float _radius;
        [Tooltip("Скорость с которой лут летит в сторону игрока")]
        [SerializeField] private float _pickupSpeed;
        //[SerializeField] private LayerMask _lootLayer;

        //[Header("Для тестов")] 
        //[Tooltip("Изменяет цвет радиуса притягивания в окне редактора. В окне Game его не видно")]
        //[SerializeField] private Color _radiusColor;

        private Transform _t;
        private Detector _detector;
        private float _timeSinceLastUpdate;
        private List<ICollectable> _detectedLoot = new List<ICollectable>();

        private bool _isEnabled = false;

        private void Awake()
        {
            _t = transform;
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
                if (detected[i].TryGetComponent(out ICollectable collectible))
                {
                    if (_detectedLoot.Contains(collectible)) continue;
                    
                    Vector2 direction = (transform.position - detected[i].transform.position).normalized;
                    collectible.Collect(direction, RegisterCollectible);
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
                Vector2 direction = (_t.position - _detectedLoot[i].Transform.position).normalized;
                
                _detectedLoot[i].Transform.position += (Vector3) (direction * (_pickupSpeed * Time.deltaTime));

                if (Vector3.Distance(_t.position, _detectedLoot[i].Transform.position) < 0.1f)
                {
                    _detectedLoot[i].Consume();
                    _detectedLoot.Remove(_detectedLoot[i]);
                }
            }
        }
    }
}