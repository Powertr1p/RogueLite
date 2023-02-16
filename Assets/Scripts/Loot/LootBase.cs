using UnityEngine;

namespace PowerTrip
{
    public abstract class LootBase : MonoBehaviour
    {
        #region Fields
        [SerializeField] private LootType _type;
        #endregion

        #region Accessors
        public LootType Type { get => _type; }
        #endregion

        public abstract void Consume();
    }
}