using System;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    public class WalkingState : IPlayerState
    {
        Vector3 GetAccelDir(Transform transform)
        {
            return transform.rotation * new Vector3(
                Input.GetAxisRaw("Horizontal"),
                0,
                Input.GetAxisRaw("Vertical")
            );
        }

        public void FixedUpdate(PlayerController parent)
        {
            Vector3 prevVelocity = parent.rigidbody.velocity;
            if (parent.GetPlayerModule<PlayerGroundedModule>().Grounded)
            {
                // Grounded
                prevVelocity = MoveGround(
                    GetAccelDir(parent.transform), 
                    prevVelocity, 
                    parent.groundAccelerate, 
                    parent.maxVelocityGround, 
                    parent.frictionConstant);
            }
            else
            {
                // In air
                prevVelocity = MoveAir(
                    GetAccelDir(parent.transform), 
                    prevVelocity, 
                    parent.airAccelerate, 
                    parent.maxVelocityAir);
            }

            // Exit condition
            if (Math.Abs(Input.GetAxisRaw("Horizontal")) < 0.001f
                && Math.Abs(Input.GetAxisRaw("Vertical")) < 0.001f)
            {
                parent.PopState();
            }
            parent.rigidbody.velocity = prevVelocity;
        }

        Vector3 CalcAcceleration(Vector3 accelDir, Vector3 prevVelocity, float accelerate, float maxVelocity)
        {
            float projVel = Vector3.Dot(prevVelocity, accelDir);
            float accelVel = accelerate * Time.fixedDeltaTime;

            if (projVel + accelVel > maxVelocity)
                accelVel = maxVelocity - projVel;

            return prevVelocity + accelDir * accelVel;
        }

        Vector3 MoveGround(Vector3 accelDir, Vector3 prevVelocity, float groundAccelerate, float maxVelocityGround, float frictionConstant)
        {
            return CalcAcceleration(accelDir, prevVelocity, groundAccelerate, maxVelocityGround);
        }

        Vector3 MoveAir(Vector3 accelDir, Vector3 prevVelocity, float airAccelerate, float maxVelocityAir)
        {
            return CalcAcceleration(accelDir, prevVelocity, airAccelerate, maxVelocityAir);
        }
    }
}
