using Patterns.ScriptableEvent;
using Patterns.ServiceLocator;
using Runtime.Components;
using Runtime.GameFlow;
using Runtime.World;
using UnityEngine;

namespace Runtime.Character
{
    public class PlayerCharacter : Character, IResettableGameSystem
    {
        [Header("Dimension Switching / Rails")] [SerializeField]
        private float dimensionSwitchCooldown = 0.3f;

        [SerializeField] private float physicalRailX = -2f;
        [SerializeField] private float mirrorRailX = 2f;
        [SerializeField] private float railSwitchSpeed = 8f; // m/s lateral

        [Header("Slide")] [SerializeField] private float slideSpeedMultiplier = 1.5f;
        [SerializeField] private float slideDuration = 0.5f;
        [SerializeField] private float slideCooldown = 0.2f; // minimum gap before another slide can start

        [Header("Auto-forward")] [SerializeField]
        private float forwardSpeed = 6f;

        [SerializeField] private bool autoRunEnabled = true;

        private DimensionType currentDimension = DimensionType.Physical;
        private float switchCooldownTimer;
        private float slideTimer;
        private bool isSliding;
        private float slideCooldownTimer;
        
        private bool isTransitioningLane;
        private float laneTransitionTargetX;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        public DimensionType CurrentDimension => currentDimension;
        public bool IsSliding => isSliding;

        private IGameManager gameManager;

        [Header("Scriptable Events")]
        [SerializeField] private ScriptableEvent onJumpEvent;
        [SerializeField] private ScriptableEvent onSlideEvent;
        [SerializeField] private ScriptableEvent onAttackEvent;

        protected override void Awake()
        {
            base.Awake();
            
            initialPosition = transform.position;
            initialRotation = transform.rotation;

            SetMotor(GetComponent<RigidbodyMotor>());
        }

        private void Start()
        {
            gameManager = ServiceLocator.Get<IGameManager>();
        }

        protected override void Update()
        {
            if (gameManager == null || gameManager.CurrentState != GameState.Playing)
                return;

            base.Update();

            TickTimers();
            ApplyCombinedMovement();
        }

        private void TickTimers()
        {
            if (switchCooldownTimer > 0f)
                switchCooldownTimer -= Time.deltaTime;
            
            if (slideCooldownTimer > 0f)
                slideCooldownTimer -= Time.deltaTime;

            if (isSliding)
            {
                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0f)
                {
                    isSliding = false;
                    slideCooldownTimer = slideCooldown;
                    motor?.SetHeightScale(1f);
                }
            }
        }
        
        /// <summary>
        /// Combines forward auto-run with lateral rail-steering into a single
        /// movement vector, sent to the motor in one call per frame.
        /// </summary>
        private void ApplyCombinedMovement()
        {
            Vector3 combined = Vector3.zero;

            if (autoRunEnabled)
            {
                float currentForwardSpeed = isSliding ? forwardSpeed * slideSpeedMultiplier : forwardSpeed;
                combined += transform.forward * currentForwardSpeed;
            }

            combined += CalculateLaneTransitionMovement();

            if (combined.sqrMagnitude > 0.0001f)
            {
                Move(combined.normalized, combined.magnitude);
            }
        }
        
        /// <summary>
        /// Only produces lateral movement while actively transitioning between lanes.
        /// Does nothing once arrived, and does nothing at all if no transition is in progress.
        /// </summary>
        private Vector3 CalculateLaneTransitionMovement()   
        {
            if (!isTransitioningLane) return Vector3.zero;

            float currentX = transform.position.x;
            float diff = laneTransitionTargetX - currentX;
            float distance = Mathf.Abs(diff);
            if (distance < 0.2f)
            {
                // Arrived: snap exactly onto the rail and stop transitioning.
                Vector3 snapPos = transform.position;
                snapPos.x = laneTransitionTargetX;
                transform.position = snapPos;

                isTransitioningLane = false;
                motor?.StopLateralMomentum();
                return Vector3.zero;
            }

            float direction = Mathf.Sign(diff);
            float maxDistanceThisFrame = railSwitchSpeed * Time.deltaTime;
            float moveDistance = Mathf.Min(maxDistanceThisFrame, distance);
            float speedThisFrame = moveDistance / Time.deltaTime;

            return new Vector3(direction * speedThisFrame, 0f, 0f);
        }
        // ---- Called by PlayerInputHandler ----

        public void RequestSwitchDimension(DimensionType desiredDimension)
        {
            if (switchCooldownTimer > 0f)
                return;

            if (currentDimension == desiredDimension)
                return;

            currentDimension = desiredDimension;

            laneTransitionTargetX = desiredDimension == DimensionType.Physical ? physicalRailX : mirrorRailX;

            isTransitioningLane = true;
            switchCooldownTimer = dimensionSwitchCooldown;

            ServiceLocator.Get<IWorldState>()?.SetDimension(desiredDimension);

            OnDimensionSwitched();
        }

        public void RequestJump() 
        {
            Jump();
            onJumpEvent?.Raise();
        } 

        public void RequestSlide(bool isPressed)
        {
            if (isPressed)
            {
                if (!IsGrounded || isSliding || slideCooldownTimer > 0f) return;
                isSliding = true;
                slideTimer = slideDuration;
                motor?.SetHeightScale(0.5f);
                Anim?.SetTrigger("OnSlide");
                onSlideEvent?.Raise();
            }
        }

        public void RequestAttack()
        {
            Anim?.SetTrigger("OnAttack");
            onAttackEvent?.Raise();
        }

        private void OnDimensionSwitched()
        {
            Anim?.SetTrigger("OnSwitchDimension");
        }

        public void ResetSystem()
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;

            currentDimension = DimensionType.Physical;
            laneTransitionTargetX = physicalRailX;

            isTransitioningLane = false;
            isSliding = false;
            switchCooldownTimer = 0f;
            slideTimer = 0f;

            motor?.SetHeightScale(1f);

            ServiceLocator.Get<IWorldState>()?.SetDimension(DimensionType.Physical);
        }
    }
}