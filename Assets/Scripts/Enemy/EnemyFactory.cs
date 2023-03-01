using System;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using Random = UnityEngine.Random;

namespace PowerTrip
{
    public sealed class EnemyFactory : MonoBehaviour
    {
        [SerializeField] private List<EnemyBase> _enemyPrefabs;
        [SerializeField] private LootFactory _lootFactory;

        public Action<EnemyBase> EnemyCreated;

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
            
            EnemyCreated?.Invoke(instance);

            return instance;
        }

        private LootBase GetLoot(LootType type)
        {
            var instance = _lootFactory.GetLoot(type);

            return instance;
        }
    }
}