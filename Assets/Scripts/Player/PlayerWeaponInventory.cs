using System;
using System.Collections.Generic;
using UnityEngine;
using Weapons;
using Weapons.Base;

namespace Player
{
    public class PlayerWeaponInventory : MonoBehaviour
    {
        [SerializeField] private int _weaponLimit = 5;
        [SerializeField] private List<Weapon> _currentWeapons;
        [SerializeField] private Transform _weaponHolder;
        [SerializeField] private Weapon _testWeapon;

        private void Update()
        {
            
        }

        public void AddWeapon(Weapon weapon)
        {
            if (_currentWeapons.Count >= _weaponLimit) return;

            var instance = Instantiate(weapon, _weaponHolder);
            
            _currentWeapons.Add(weapon);
        }
    }
}