using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IWorldController
    {
        void ElevateLines(float distance, out Vector3 leftLine, out Vector3 rightLine);
        void LaodWorldUpTo(float distance);
    }
}