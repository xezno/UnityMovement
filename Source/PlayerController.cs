using Assets.Scripts.Player.States;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerController : MonoBehaviour
    {
        private const int PLAYER_STATE_STACK_SIZE = 5;
        private IPlayerState[] playerStateStack = new IPlayerState[PLAYER_STATE_STACK_SIZE];
        private int playerStateStackIndex = -1;

        #region Properties
        [Header("Player Properties")]
        public float raycastOffset = -0.4f;
        public float raycastingThreshold = 0.2f;
        public float gravity = 20f;
        public float terminalVelocity = 50.0f;
        public float frictionConstant = 4f;
        public float jumpForce = 6.5f;
        public float mouseSensitivity = 5f;

        public float maxAngle = 80f;
        public float maxSpeed = 6.09600f; // TODO

        [Header("Air movement")]
        public float maxVelocityAir = 5f; // Ground / friction
        public float airAccelerate = 15f;

        [Header("Ground movement")]
        public float maxVelocityGround = 80f;
        public float groundAccelerate = 40f;
        #endregion

        [HideInInspector] public Transform transform;
        [HideInInspector] public Rigidbody rigidbody;
        [HideInInspector] public CapsuleCollider capsuleCollider;

        private List<BasePlayerModule> playerModules = new List<BasePlayerModule>();

        private void Start()
        {
            // Setup player info
            transform = this.GetComponent<Transform>();
            rigidbody = this.GetComponent<Rigidbody>();
            capsuleCollider = this.GetComponent<CapsuleCollider>();

            // Setup player modules
            // TODO: Add to inspector
            // playerModules.Add(new PlayerSpeedLimitModule());
            playerModules.Add(new PlayerGroundedModule());
            playerModules.Add(new PlayerGravityModule());
            playerModules.Add(new PlayerFrictionModule());
            playerModules.Add(new PlayerRotationModule());
            playerModules.Add(new PlayerJumpModule());

            // Default state
            // TODO: Add to inspector
            PushState(new IdleState());
        }

        #region FSM
        public void PushState(IPlayerState state)
        {
            Debug.Log($"Pushing state {state.GetType()} to stack");
            playerStateStack[++playerStateStackIndex] = state;
        }

        public IPlayerState PopState()
        {
            var state = playerStateStack[playerStateStackIndex--];
            Debug.Log($"Popping state {state.GetType()} from stack");
            return state;
        }
        #endregion

        #region Modules
        public T GetPlayerModule<T>() where T : BasePlayerModule
        {
            foreach (var module in playerModules)
            {
                if (module.GetType() == typeof(T))
                {
                    return module as T;
                }
            }

            throw new Exception($"No module of type {typeof(T).Name} on this player.");
        }
        #endregion

        private void FixedUpdate()
        {
            playerStateStack[playerStateStackIndex].FixedUpdate(this);

            foreach (var module in playerModules)
            {
                module.FixedUpdate(this, Time.fixedDeltaTime);
            }
        }

        private void Update()
        {
            foreach (var module in playerModules)
            {
                module.Update(this, Time.deltaTime);
            }
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
            GUI.Box(new Rect(16, 16, 320, 480), text, new GUIStyle(GUI.skin.box) { alignment = TextAnchor.UpperLeft });
        }
    }
}
