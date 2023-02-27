using System.Collections.Generic;
using System.Linq;

using UnityEngine;

namespace PowerTrip
{
    public sealed class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<EnemyBase> _enemyPrefabs;
        [SerializeField] private LootFactory _lootFactory;
        [SerializeField] private BloodPool _bloodPool;

        public EnemyBase GetEnemy(EnemyType type, Player player)
        {
            var instance = Instantiate(_enemyPrefabs.FirstOrDefault(x => x.Type == type));
            
            if (Random.Range(1f, 100f) <= instance.DropChance)
            {
                instance.Initialize(player, GetLoot(instance.LootType));
            }
            else
            {
                instance.Initialize(player);
            }

            return instance;
        }

        private LootBase GetLoot(LootType type)
        {
            var instance = _lootFactory.GetLoot(type);

            return instance;
        }
    }
}