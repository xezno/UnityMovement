using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        private bool canJump = true;
        private float jumpTimeThreshold = 0.03f; // In seconds
        private float timeSinceLastJump = 0;

        public void ApplyJump()
        {
            // Jump timer logic
            if (!canJump)
            {
                timeSinceLastJump += Time.deltaTime;
                if (timeSinceLastJump > jumpTimeThreshold)
                {
                    canJump = true;
                    timeSinceLastJump = 0;
                }
            }

            // Jump check logic
            if (Grounded && GetJump() && canJump)
            {
                Debug.Log("Jump");
                var vel = rigidbody.velocity;
                vel.y += jumpForce;
                rigidbody.velocity = vel;

                canJump = false;
            }
        }

        bool GetJump()
        {
            return Input.GetButton("Jump");
        }
    }
}
