using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGravityModule : BasePlayerModule
    {
        public override void FixedUpdate(PlayerController parent, float deltaTime)
        {
            if (!parent.GetPlayerModule<PlayerGroundedModule>().Grounded)
            {
                var vel = parent.rigidbody.velocity;
                vel.y = CalcGravity(vel.y, parent.gravity, parent.terminalVelocity);

                parent.rigidbody.velocity = vel;
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
