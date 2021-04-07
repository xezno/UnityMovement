using Assets.Scripts.Player.States;
using UnityEngine;
using Sirenix.OdinInspector;

namespace Assets.Scripts.Player
{
    public partial class Player : MonoBehaviour
    {
        private const int StateStackSize = 5;
        private IPlayerState[] playerStateStack = new IPlayerState[StateStackSize];
        private int playerStateStackIndex = -1;

        #region Properties
        public LayerMask groundedLayerMask;
        public float raycastOffset = -0.4f;
        public float raycastingThreshold = 0.2f;

        [BoxGroup("Movement properties")]
        [Tooltip("Applied when player isn't grounded")] public float gravity = 20f;
        [Tooltip("Max gravity")] public float terminalVelocity = 50.0f;
        [Tooltip("Controls how slippery the player is")] public float frictionConstant = 4f;
        [Tooltip("Vertical velocity applied on jump")] public float jumpForce = 6.5f;
        public float mouseSensitivity = 5f;
        [Tooltip("Max strafe angle. Use 180 for Quake classic-like bhops")] public float maxAngle = 180f;

        [BoxGroup("Air movement")]
        public float maxVelocityAir = 5f;
        public float airAccelerate = 15f;

        [BoxGroup("Ground movement")]
        public float maxVelocityGround = 80f;
        public float groundAccelerate = 40f;

        [BoxGroup("Player animation")]
        public Animator playerAnimator;
        #endregion

        [HideInInspector] public new Transform transform;
        [HideInInspector] public new Rigidbody rigidbody;
        [HideInInspector] public CapsuleCollider capsuleCollider;

        private void Start()
        {
            // Setup player info
            transform = this.GetComponent<Transform>();
            rigidbody = this.GetComponent<Rigidbody>();
            capsuleCollider = this.GetComponent<CapsuleCollider>();

            // Default state
            PushState(new IdleState());
        }

        #region FSM
        /// <summary>
        /// Push to the player state stack.
        /// </summary>
        /// <param name="state"></param>
        public void PushState(IPlayerState state)
        {
            playerStateStack[++playerStateStackIndex] = state;
        }

        /// <summary>
        /// Pop from the player state stack.
        /// </summary>
        /// <returns></returns>
        public IPlayerState PopState()
        {
            var state = playerStateStack[playerStateStackIndex--];
            return state;
        }
        #endregion

        private void FixedUpdate()
        {
            playerStateStack[playerStateStackIndex].FixedUpdate(this);

            ApplyFriction();
            ApplyRotation();
            ApplyJump();
        }

        private void Update()
        {
            ApplyAnimation();
            ApplyGroundCheck();
            ApplyGravity();
        }

        private void OnGUI()
        {
            var text = "Player Controller\n";
            for (int i = 0; i <= playerStateStackIndex; ++i)
            {
                if (i == playerStateStackIndex)
                    text += "--> ";
                text += $"{i}: {playerStateStack[i].GetType()}\n";
            }

            text += $"Grounded? {Grounded}";
            GUI.Box(new Rect(16, 16, 320, 480), text, new GUIStyle(GUI.skin.box) { alignment = TextAnchor.UpperLeft });
        }
    }
}
