using UnityEngine;

namespace TEDinc.LinesRunner
{
    public abstract class CollectableBase : MonoBehaviour, ICollectable
    {
        public virtual string playerPrefsName { get; }
        public bool collected { get; protected set; }

        public virtual void Collect()
        {
            if (!collected)
                Debug.Log($"[CLC] Collected: {playerPrefsName}");
            collected = true;
            Destroy(gameObject);
        }
    }
}