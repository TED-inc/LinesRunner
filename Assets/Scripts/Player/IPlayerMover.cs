using UnityEngine;

namespace TEDinc.LinesRunner
{
    public interface IPlayerMover
    {
        Pose Move(float distance, float widthElevation);
    }
}