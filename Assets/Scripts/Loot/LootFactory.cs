using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace PowerTrip
{
    public sealed class LootFactory : MonoBehaviour
    {
        [SerializeField] private List<LootBase> _lootPrefabs;

        public LootBase GetLoot(LootType type)
        {
            var instance = Instantiate(_lootPrefabs.FirstOrDefault(x => x.Type == type));
            return instance;
        }
    }
}