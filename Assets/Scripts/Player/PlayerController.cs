#pragma warning disable 0649 //fix private SerializeField "will not be assigned" error
using UnityEngine;

namespace TEDinc.LinesRunner
{
    /// <summary>
    /// player instance. Controls all its moves, collisions and triggers
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public sealed class PlayerController : MonoBehaviour
    {
        [SerializeField]
        private PhysicsEventsDetector collisionDetector;
        [SerializeField]
        private PhysicsEventsDetector collectableDetector;
        private CharacterController characterController;
        private IPlayerMover playerMover;
        private IPlayerJumper playerJumper;


        /// <summary>
        /// necessary call on create/recreate
        /// </summary>
        public void Init()
        {
            if (characterController == null)
                characterController = GetComponent<CharacterController>();

            playerMover = new PlayerMover();
            playerJumper = new PlayerJumper(characterController);

            collisionDetector.onTriggerEnter.AddListener(CheckCollision);
            collectableDetector.onTriggerEnter.AddListener(CheckCollectable);

            GameRunningDelegateController.instance.onFixedUpdateWhileRunning.Add(Move);

            characterController.Move(Vector3.zero);
            Move();
        }

        private void Move()
        {
            Pose pose = playerMover.Move();
            transform.rotation = pose.rotation;
            characterController.Move(new Vector3(pose.position.x, playerJumper.addHeight, pose.position.z) - characterController.transform.position);
            GameDataController.instance.InvokeByPlayerPrefs(GameConst.PlayerPrefs.distance);
        }

        private void CheckCollision(Collider collider)
        {
            if (collider.gameObject.Equals(gameObject) || collider.transform.parent.Equals(transform))
                return;
            if (TryCollect(collider))
                return;

            GameFlowController.instance.HitPlayer();
        }

        private void CheckCollectable(Collider collider)
        {
            TryCollect(collider);
        }

        private bool TryCollect(Collider collider)
        {
            ICollectable collectable = collider.GetComponent<ICollectable>();
            if (collectable != null)
                collectable.Collect();

            return collectable != null;
        }
    }
}