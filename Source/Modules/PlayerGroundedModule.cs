using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerGroundedModule : BasePlayerModule
    {
        public bool Grounded { get; private set; }

        public override void FixedUpdate(PlayerController parent, float deltaTime)
        {
            Grounded = CheckGrounded(parent);
        }

        private bool CheckGrounded(PlayerController player)
        {
            return CheckGrounded(player, out _);
        }

        private bool CheckGrounded(PlayerController player, out RaycastHit hitInfo)
        {
            var sourcePosition = player.transform.position + (player.raycastOffset * -Vector3.down);
            Debug.DrawRay(sourcePosition, Vector3.down * player.raycastingThreshold, Color.blue);
            Debug.DrawRay(sourcePosition, Vector3.down * (player.capsuleCollider.radius), Color.green);
            return Physics.SphereCast(sourcePosition, player.capsuleCollider.radius, Vector3.down, out hitInfo, player.raycastingThreshold);
        }
    }
}
