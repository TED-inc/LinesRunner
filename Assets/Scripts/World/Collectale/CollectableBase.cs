using UnityEngine;

/// <summary>
/// base implementation of ICollectable:
/// collecatable items in game
/// </summary>
namespace TEDinc.LinesRunner
{
    public abstract class CollectableBase : MonoBehaviour, ICollectable
    {
        public virtual string playerPrefsName { get; }
        public bool collected { get; protected set; }

        public virtual void Collect() =>
            Destroy(gameObject);
    }
}