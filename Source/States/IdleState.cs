using System;
using UnityEngine;

namespace Assets.Scripts.Player.States
{
    class IdleState : IPlayerState
    {
        public void FixedUpdate(PlayerController parent)
        {
            // Exit condition
            if (Math.Abs(Input.GetAxisRaw("Horizontal")) > 0.001f
                || Math.Abs(Input.GetAxisRaw("Vertical")) > 0.001f)
            {
                parent.PushState(new WalkingState());
            }
        }
    }
}
