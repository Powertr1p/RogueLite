using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace PowerTrip
{
    public class BloodPool : MonoBehaviour
    {
        [SerializeField] private EnemyFactory _enemyFactory;
        
        [SerializeField] private int _limit = 50;
        [SerializeField] private BloodAnimation _prefab;

        private Queue<BloodAnimation> _pool = new Queue<BloodAnimation>();
        
        private void OnEnable()
        {
            _enemyFactory.EnemyCreated += OnEnemyCreated;
        }

        private void OnDisable()
        {
            _enemyFactory.EnemyCreated -= OnEnemyCreated;
        }

        private void Start()
        {
            Spawn();
        }

        private void Spawn()
        {
            for (int i = 0; i < _limit; i++)
            {
                var instance = Instantiate(_prefab, transform);
                instance.Initialize();
                _pool.Enqueue(instance);
            }
        }

        private void OnEnemyCreated(EnemyBase enemy)
        {
            enemy.OnDeath += SpawnBlood;
        }

        private void SpawnBlood(EnemyBase enemy)
        {
            var instance = Dequeue();
            _pool.Enqueue(instance);

            if (instance.gameObject.activeSelf)
               ReuseBehaviour(instance, enemy.transform.position);
            else
                EnableObject(instance, enemy.transform.position);
            
            enemy.OnDeath -= SpawnBlood;
        }

        private BloodAnimation Dequeue()
        {
            return _pool.Dequeue();
        }

        private void ReuseBehaviour(BloodAnimation instance, Vector3 targetPosition)
        {
            instance.Deactivation(instance.Activate);
            instance.transform.position = targetPosition;
        }

        private void EnableObject(BloodAnimation instance, Vector3 targetPosition)
        {
            instance.transform.position = targetPosition;
            instance.Activate();
        }
    }
}