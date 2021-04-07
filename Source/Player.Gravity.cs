using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        public void ApplyGravity()
        {
            if (!Grounded)
            {
                var vel = rigidbody.velocity;
                vel.y = CalcGravity(vel.y, gravity, terminalVelocity);

                rigidbody.velocity = vel;
            }
        }

        float CalcGravity(float yVel, float gravity, float terminalVelocity)
        {
            yVel -= gravity * Time.fixedDeltaTime;
            yVel = Mathf.Max(yVel, -(gravity * terminalVelocity));
            return yVel;
        }
    }
}