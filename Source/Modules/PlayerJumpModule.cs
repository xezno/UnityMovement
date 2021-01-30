using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerJumpModule : BasePlayerModule
    {
        private bool canJump = true;
        private float jumpTimeThreshold = 0.03f; // In seconds
        private float timeSinceLastJump = 0;

        public override void FixedUpdate(PlayerController parent, float deltaTime)
        {
            // Jump timer logic
            if (!canJump)
            {
                timeSinceLastJump += deltaTime;
                if (timeSinceLastJump > jumpTimeThreshold)
                {
                    canJump = true;
                    timeSinceLastJump = 0;
                }
            }

            // Jump check logic
            if (parent.GetPlayerModule<PlayerGroundedModule>().Grounded && GetJump() && canJump)
            {
                Debug.Log("Jump");
                var vel = parent.rigidbody.velocity;
                vel.y += parent.jumpForce;
                parent.rigidbody.velocity = vel;

                canJump = false;
            }
        }

        bool GetJump()
        {
            return Input.GetButton("Jump");
        }
    }
}
