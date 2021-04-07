using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        public void ApplyFriction()
        {
            if (Grounded)
            {
                var vel = rigidbody.velocity;
                vel = CalcFriction(vel, frictionConstant);

                rigidbody.velocity = vel;
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
