using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerFrictionModule : BasePlayerModule
    {
        public override void FixedUpdate(PlayerController parent, float deltaTime)
        {
            if (parent.GetPlayerModule<PlayerGroundedModule>().Grounded)
            {
                var vel = parent.rigidbody.velocity;
                vel = CalcFriction(vel, parent.frictionConstant);

                parent.rigidbody.velocity = vel;
            }
        }

        Vector3 CalcFriction(Vector3 prevVelocity, float frictionConstant)
        {
            float speed = prevVelocity.magnitude;

            if (speed > 0)
            {
                float drop = speed * frictionConstant * Time.fixedDeltaTime;
                return prevVelocity * Mathf.Max(speed - drop, 0) / speed;
            }

            return prevVelocity;
        }
    }
}
