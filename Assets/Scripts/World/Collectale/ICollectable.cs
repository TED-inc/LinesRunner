using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface ICollectable
    {
        string playerPrefsName { get; }
        bool collected { get; }
        void Collect();  
    }
}