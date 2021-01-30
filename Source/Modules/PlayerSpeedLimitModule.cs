using UnityEngine;

namespace Assets.Scripts.Player
{
    /*
     * TODO:
     * - Fix
     */ 
    public class PlayerSpeedLimitModule : BasePlayerModule
    {
        public override void FixedUpdate(PlayerController parent, float deltaTime)
        {
            var velocity = parent.rigidbody.velocity;
            var originalVelocity = parent.rigidbody.velocity;
            velocity.y = 0;

            if (velocity.sqrMagnitude > Mathf.Pow(parent.maxSpeed, 2))
            {
                var newVelocity = originalVelocity - (originalVelocity / 1.5f);
                newVelocity.y = originalVelocity.y;

                parent.rigidbody.velocity = newVelocity;
            }
        }
    }
}
