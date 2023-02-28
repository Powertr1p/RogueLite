using System;
using UnityEngine;

namespace PowerTrip.PickableWeapons
{
    public class PickableWeapon : MonoBehaviour, ICollectable
    {
        [SerializeField] private Weapon _getWeaponPrefabPrefab;

        public Weapon GetWeaponPrefab => _getWeaponPrefabPrefab;
        
        public Transform Transform => transform;

        public void Collect(Vector2 direction, Action<ICollectable> onComplete)
        {
            onComplete?.Invoke(this);
        }

        public void Consume()
        {
           Destroy(gameObject);
        }
    }
}