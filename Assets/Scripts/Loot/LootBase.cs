using UnityEngine;

namespace Loot
{
    public class LootBase : MonoBehaviour
    {
        [SerializeField] private LootType _type;

        public LootType GetType => _type;
    }
}