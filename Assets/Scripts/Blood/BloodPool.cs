using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

namespace PowerTrip
{
    public class BloodPool : MonoBehaviour
    {
        [SerializeField] private int _limit = 50;
        [SerializeField] private List<BloodAnimation> _pool;
        [SerializeField] private BloodAnimation _prefab;

        private void Start()
        {
            _pool = new List<BloodAnimation>();
        }

        private void Spawn()
        {
            for (int i = 0; i < _limit; i++)
            {
                var instance = Instantiate(_prefab, transform);
                _pool.Add(instance);
            }
        }
    }
}