#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PhysicsEventsDetector physicsEventsDetector;
        private CharacterController characterController;
        private IPlayerMover playerMover;

        public void Init(IPlayerMover playerMover)
        {
            this.playerMover = playerMover;
            physicsEventsDetector.onTriggerEnter.AddListener(CheckCollision);
        }

        public void Move(float distance, float widthElevation)
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();

            Pose pose = playerMover.Move(distance, widthElevation);
            transform.rotation = pose.rotation;
            characterController.Move(pose.position - characterController.transform.position);
        }

        private void CheckCollision(Collider collider)
        {
            if (collider.gameObject.Equals(gameObject))
                return;

            Debug.Log($"Collide with: {collider.name}");
        }
    }
}