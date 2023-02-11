using UnityEngine;

namespace Loot
{
    public abstract class LootBase : MonoBehaviour
    {
        [SerializeField] private LootType _type;

        public LootType GetType => _type;

        public abstract void Consume();
    }
}