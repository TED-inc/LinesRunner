using UnityEngine;

namespace TEDinc.LinesRunner
{
    public sealed class PlayerJumper : IPlayerJumper
    {
        public float addHeight { get; private set; } = 0f;
        private CharacterController characterController;

        private float ySpeed = 0f;

        private bool tryJump;

        private void Jump()
        {
            tryJump = true;
        }

        private void OnFixedUpdate()
        {
            if (characterController.isGrounded)
            {
                ySpeed = -1f;
                if (tryJump)
                    ySpeed = GameConst.playerJumpSpeed;
            }

            ySpeed += Physics.gravity.y * GameConst.playerGravityMultipyaer * Time.deltaTime;
            addHeight = ySpeed * Time.deltaTime + characterController.transform.position.y;
            tryJump = false;
        }

        public PlayerJumper(CharacterController characterController)
        {
            this.characterController = characterController;
            GameRunnerController.instance.inputController.OnJump.AddListener(Jump);
            GameRunningDelegateController.instance.onFixedUpdateWhileRunning.Add(OnFixedUpdate);
        }
    }
}