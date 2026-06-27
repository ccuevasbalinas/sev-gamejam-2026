using Runtime.Components;
using Runtime.Enumerators;
using UnityEngine;

namespace Runtime.Character
{
    public class PlayerCharacter : Character
    {
        [Header("Dimension Switching / Rails")] [SerializeField]
        private float dimensionSwitchCooldown = 0.3f;

        [SerializeField] private float physicalRailX = -2f;
        [SerializeField] private float mirrorRailX = 2f;
        [SerializeField] private float railSwitchSpeed = 8f; // m/s lateral

        [Header("Slide")] [SerializeField] private float slideSpeedMultiplier = 1.5f;
        [SerializeField] private float slideDuration = 0.5f;

        [Header("Auto-forward")] [SerializeField]
        private float forwardSpeed = 6f;

        [SerializeField] private bool autoRunEnabled = true;

        private DimensionTarget currentDimension = DimensionTarget.Physical;
        private float switchCooldownTimer;
        private float slideTimer;
        private bool isSliding;

        private bool isTransitioningLane;
        private float laneTransitionTargetX;
        
        public DimensionTarget CurrentDimension => currentDimension;
        public bool IsSliding => isSliding;

        protected override void Awake()
        {
            base.Awake();
            SetMotor(GetComponent<RigidbodyMotor>());
        }

        protected override void Update()
        {
            base.Update();

            TickTimers();
            ApplyCombinedMovement();
        }

        private void TickTimers()
        {
            if (switchCooldownTimer > 0f)
                switchCooldownTimer -= Time.deltaTime;

            if (isSliding)
            {
                slideTimer -= Time.deltaTime;
                if (slideTimer <= 0f)
                {
                    isSliding = false;
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
            if (distance < 0.6f)
            {
                // Arrived: snap exactly onto the rail and stop transitioning.
                Vector3 snapPos = transform.position;
                snapPos.x = laneTransitionTargetX;
                transform.position = snapPos;

                isTransitioningLane = false;
                return Vector3.zero;
            }

            float direction = Mathf.Sign(diff);
            float maxDistanceThisFrame = railSwitchSpeed * Time.deltaTime;
            float moveDistance = Mathf.Min(maxDistanceThisFrame, distance);
            float speedThisFrame = moveDistance / Time.deltaTime;

            return new Vector3(direction * speedThisFrame, 0f, 0f);
        }
        // ---- Called by PlayerInputHandler ----

        public void RequestSwitchDimension(DimensionTarget DesiredDimension)
        {
            if (switchCooldownTimer > 0f) return;

            if (currentDimension == DesiredDimension) return;

            currentDimension = DesiredDimension;
            laneTransitionTargetX = DesiredDimension == DimensionTarget.Physical ? physicalRailX : mirrorRailX;
            isTransitioningLane = true;
            switchCooldownTimer = dimensionSwitchCooldown;
            OnDimensionSwitched();
        }

        public void RequestJump() => Jump();

        public void RequestSlide(bool isPressed)
        {
            if (isPressed)
            {
                if (!IsGrounded) return;
                isSliding = true;
                slideTimer = slideDuration;
                motor?.SetHeightScale(0.5f);
            }
        }

        public void RequestAttack()
        {
            Anim?.SetTrigger("Attack");
        }

        private void OnDimensionSwitched()
        {
            Anim?.SetTrigger("SwitchDimension");
        }
    }
}