using UnityEngine;
using UnityEditor;

namespace TEDinc.LinesRunner.Tools
{
    public sealed class ClearPlayerPrefs
    {
        [MenuItem(nameof(Tools) + "/" + nameof(ClearPlayerPrefs))]
        private static void Clear() =>
            PlayerPrefs.DeleteAll();
    }
}