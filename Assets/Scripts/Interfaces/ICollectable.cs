using System;
using UnityEngine;

namespace PowerTrip
{
    public interface ICollectable
    {
        void Collect(Vector2 direction, Action<ICollectable> onComplete);
        void Consume();
        Transform Transform { get; }
    }
}