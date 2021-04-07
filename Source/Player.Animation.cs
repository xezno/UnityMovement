using UnityEngine;

namespace Assets.Scripts.Player
{
    public partial class Player
    {
        public void ApplyAnimation()
        {
            playerAnimator.SetFloat("MoveX", Input.GetAxisRaw("Horizontal"), 0.1f, Time.deltaTime);
            playerAnimator.SetFloat("MoveY", Input.GetAxisRaw("Vertical"), 0.1f, Time.deltaTime);
        }
    }
}
