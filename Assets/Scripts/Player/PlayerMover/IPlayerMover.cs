using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// controls player horizontal movement
    /// </summary>
    public interface IPlayerMover
    {
        /// <summary>
        /// returns horizontal of player
        /// </summary>
        Pose Move();
    }
}