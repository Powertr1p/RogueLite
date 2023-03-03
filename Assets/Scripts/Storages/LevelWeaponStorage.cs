using System.Collections.Generic;
using PowerTrip;
using UnityEngine;

namespace Storages
{
    public class LevelWeaponStorage : MonoBehaviour
    {
        [SerializeField] private List<Weapon> _availableWeapons;

        public Weapon GetSingleWeapon()
        {
            return _availableWeapons[Random.Range(0, _availableWeapons.Count)];
        }

        public List<Weapon> GetThreeWeapons()
        {
            int neededCount = 3;
            
            List<Weapon> selectedItems = new List<Weapon>();
            
            while (selectedItems.Count < neededCount)
            {
                int randomIndex = Random.Range(0, _availableWeapons.Count);
                Weapon item = _availableWeapons[randomIndex];

                if (selectedItems.Contains(item)) continue;
                selectedItems.Add(item);
            }

            return selectedItems;
        }
    }
}