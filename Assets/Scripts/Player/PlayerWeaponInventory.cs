using System.Collections.Generic;
using UnityEngine;

namespace PowerTrip
{
    public class PlayerWeaponInventory : MonoBehaviour
    {
        [SerializeField] private int _weaponLimit = 5;
        [SerializeField] private List<Weapon> _currentWeapons;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private Weapon _testWeapon;

        public void AddWeapon(Weapon weapon)
        {
            if (_currentWeapons.Count >= _weaponLimit) return;

            var instance = Instantiate(weapon, _weaponHolder);
            
            _currentWeapons.Add(weapon);
        }
    }
}