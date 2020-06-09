using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IWorldController
    {
        Pose ElevatePose(float distance, float t);
        void ElevateLines(float distance, out Vector3 leftLine, out Vector3 rightLine);
        void LoadWorldUpTo(float from, float to);
        void HideWorldBefore(float distance);
    }
}