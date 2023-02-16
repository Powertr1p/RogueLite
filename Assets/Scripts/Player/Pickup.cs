using System.Collections.Generic;
using Loot;
using UnityEngine;

namespace Player
{
    public class Pickup : MonoBehaviour
    {
        [Tooltip("Радиус, в котором игрок может поднимать предметы")]
        [Range(0f, 100f)]
        [SerializeField] private float _radius;
        [Tooltip("Скорость с которой лут летит в сторону игрока")]
        [SerializeField] private float _pickupSpeed;
        [SerializeField] private LayerMask _lootLayer;

        [Header("Для тестов")] 
        [Tooltip("Изменяет цвет радиуса притягивания в окне редактора. В окне Game его не видно")]
        [SerializeField] private Color _radiusColor;

        private const float DelayBetweenChecks = 1f;
        
        private Transform _transform;
        private float _timeSinceLastUpdate;
        private List<LootBase> _detectedLoot = new List<LootBase>();

        private void Awake()
        {
            _transform = transform;
        }

        private void Update()
        {
            DetectLoot();

            MoveDetectedLootTowardsPlayer();
        }

        private void DetectLoot()
        {
            Collider2D[] results = new Collider2D[10];
            var hit = Physics2D.OverlapCircleNonAlloc(_transform.position, _radius, results, _lootLayer);

            for (int i = 0; i < hit; i++)
            {
                if (results[i].TryGetComponent(out LootBase loot))
                {
                    if (_detectedLoot.Contains(loot)) continue;

                    _detectedLoot.Add(loot);
                }
            }
        }

        private void MoveDetectedLootTowardsPlayer()
        {
            for (int i = 0; i < _detectedLoot.Count; i++)
            {
                Vector2 direction = (transform.position - _detectedLoot[i].transform.position).normalized;
                _detectedLoot[i].transform.position += (Vector3) (direction * (_pickupSpeed * Time.deltaTime));

                if (Vector3.Distance(transform.position, _detectedLoot[i].transform.position) < 0.1f)
                {
                    _detectedLoot[i].Consume();
                    _detectedLoot.Remove(_detectedLoot[i]);
                }
            }
        }

        
        //TODO: enable if lags
        private bool CanCheck()
        {
            _timeSinceLastUpdate += Time.deltaTime;
            
            if (_timeSinceLastUpdate < DelayBetweenChecks) return false;
            
            _timeSinceLastUpdate = 0f;

            return true;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _radiusColor;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }
    }
}