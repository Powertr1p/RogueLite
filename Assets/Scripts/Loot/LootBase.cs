using System;
using UnityEngine;

namespace PowerTrip
{
    public abstract class LootBase : MonoBehaviour, ICollectable
    {
        #region Fields
        [SerializeField] private LootType _type;
        #endregion

        #region Accessors
        public LootType Type { get => _type; }
        public Vector3 Position => transform.position;
        #endregion

        public abstract void Collect(Vector2 direction, Action<ICollectable> onComplete);
        public abstract void Consume();
        public Transform Transform => transform;
    }
}