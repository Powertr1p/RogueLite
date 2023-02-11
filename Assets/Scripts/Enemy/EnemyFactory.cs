using System.Collections.Generic;
using System.Linq;
using Loot;
using UnityEngine;

namespace Enemy
{
    public class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<EnemyBase> _enemyPrefabs;
        [SerializeField] private LootFactory _lootFactory;

        public EnemyBase GetEnemy(EnemyType type, Transform player)
        {
            var instance = Instantiate(_enemyPrefabs.FirstOrDefault(x => x.GetType == type));
            instance.Initialize(player, GetLoot());

            return instance;
        }

        private LootBase GetLoot()
        {
            var instance = _lootFactory.GetLoot(LootType.ExpCrystal);
            return instance;
        }
    }
}