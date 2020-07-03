#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// controls player animation
    /// </summary>
    public sealed class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private Animator animator;

        private void Start()
        {
            SetAnimatorByRunningGame(GameFlowController.instance.isGameRunning);
            GameFlowController.instance.onGameRunningChange.AddListener(SetAnimatorByRunningGame);
        } 

        private void SetAnimatorByRunningGame(bool isRunning) =>
            animator.speed = isRunning ? 1f : 0f;
    }
}