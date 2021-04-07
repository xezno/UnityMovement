﻿namespace Assets.Scripts.Player.States
{
    public class WalkingState : BaseMovementState
    {
        public WalkingState(Player parent)
            : base(parent.groundAccelerate, parent.maxVelocityGround) { }
    }
}
