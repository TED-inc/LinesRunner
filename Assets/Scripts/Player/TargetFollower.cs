#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    public sealed class TargetFollower : MonoBehaviour
    {
        [SerializeField]
        private Transform target;
        [SerializeField, Range(0.01f, 1f)]
        private float followSpeed = 0.5f;
        private Vector3 offset;

        private void Start() =>
            Invoke(nameof(Setup), 0f); //call on next frame

        private void Setup()
        {
            offset = transform.position - target.position;
            GameRunningDelegateController.instance.onFixedUpdateWhileRunning.Add(LerpPose);
        }

        private void LerpPose()
        {
            transform.position = Vector3.Lerp(transform.position, target.position + target.rotation * offset, followSpeed);
            LookAtTarget();
        }

        [ContextMenu("Look at target")]
        private void LookAtTarget() =>
            transform.LookAt(target);


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (target == null)
                return;

            offset = transform.position - target.position;

            Gizmos.color = Color.cyan;
            Gizmos.DrawLine(target.position, transform.position);
        }
#endif
    }
}