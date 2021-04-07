using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        public bool Grounded { get; private set; }

        public void ApplyGroundCheck()
        {
            Grounded = CheckGrounded();
        }

        private bool CheckGrounded()
        {
            return CheckGrounded(out _);
        }

        private bool CheckGrounded(out RaycastHit hitInfo)
        {
            var sourcePosition = transform.position + (raycastOffset * -Vector3.down);
            Debug.DrawRay(sourcePosition, Vector3.down * raycastingThreshold, Color.blue);
            Debug.DrawRay(sourcePosition, Vector3.down * (capsuleCollider.radius), Color.green);
            return Physics.SphereCast(sourcePosition, capsuleCollider.radius, Vector3.down, out hitInfo, raycastingThreshold, groundedLayerMask);
        }
    }
}
