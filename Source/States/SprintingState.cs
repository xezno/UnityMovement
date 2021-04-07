namespace Assets.Scripts.Player.States
{
    public class SprintingState : BaseMovementState
    {
        public SprintingState(Player parent)
            : base(parent.groundAccelerate, parent.maxVelocityGround) { }
    }
}
