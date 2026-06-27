using Runtime.Interfaces;

namespace Runtime.Components
{
    using UnityEngine;

    [RequireComponent(typeof(Rigidbody))]
    public class RigidbodyMotor : MonoBehaviour, ICharacterMotor
    {
        [Header("Acceleration")] [SerializeField]
        private float acceleration = 20f; // m/s² while a direction is being fed in

        [SerializeField] private float deceleration = 25f; // m/s² when input stops (usually higher = snappier stop)

        [Header("Grounding")] [SerializeField] private float groundCheckDistance = 0.2f;
        [SerializeField] private LayerMask groundMask = ~0;

        private Rigidbody rb;
        private CapsuleCollider capsule;
        private Vector3 targetDirection; // normalized
        private float targetSpeed; // the speed param passed in this tick
        private Vector3 currentHorizontalVelocity; // the motor's own ramped state
        private bool jumpQueued;
        private float queuedJumpForce;
        
        private float originalHeight;
        private Vector3 originalCenter;
        
        public bool IsGrounded { get; private set; }
        public Vector3 Velocity => rb.linearVelocity;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
            capsule = GetComponent<CapsuleCollider>();
            rb.freezeRotation = true;

            originalHeight = capsule.height;
            originalCenter = capsule.center;
        }

        public void Move(Vector3 direction, float speed)
        {
            targetDirection = direction.sqrMagnitude > 0f ? direction.normalized : Vector3.zero;
            targetSpeed = speed;
        }

        public void Jump(float jumpForce)
        {
            jumpQueued = true;
            queuedJumpForce = jumpForce;
        }

        public void Tick(float deltaTime)
        {
            //IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 1f, groundMask);
            //Debug.DrawRay(transform.position, Vector3.down * (groundCheckDistance + 1f), IsGrounded ? Color.green : Color.red);
        }

        public void SetHeightScale(float scale)
        {
            float newHeight = originalHeight * scale;
            float heightReduction = originalHeight - newHeight;

            capsule.height = newHeight;
            capsule.center = originalCenter - new Vector3(0f, heightReduction / 2f, 0f);
        }

        public void StopLateralMomentum()
        {
          //  currentHorizontalVelocity = new Vector3(0f, currentHorizontalVelocity.y, 0f) 
            //                            + transform.forward * Vector3.Dot(currentHorizontalVelocity, transform.forward); 
            currentHorizontalVelocity.x = 0f;
            rb.linearVelocity = new Vector3(0f, rb.linearVelocity.y, currentHorizontalVelocity.z);
        }

        private void FixedUpdate()
        {
            Vector3 desiredVelocity = targetDirection * targetSpeed;
            bool hasInput = targetDirection.sqrMagnitude > 0f;
            float rate = hasInput ? acceleration : deceleration;
            
            IsGrounded = Physics.Raycast(transform.position, Vector3.down, groundCheckDistance + 1f, groundMask);   
            
            currentHorizontalVelocity = Vector3.MoveTowards(
                currentHorizontalVelocity,
                desiredVelocity,
                rate * Time.fixedDeltaTime
            );
            
            rb.linearVelocity = new Vector3(currentHorizontalVelocity.x, rb.linearVelocity.y, currentHorizontalVelocity.z);

            if (jumpQueued)
            {
                rb.AddForce(Vector3.up * queuedJumpForce, ForceMode.Impulse);
                jumpQueued = false;
            }

            // Reset the per-tick target so Move() must be called again each frame to keep moving
            // (matches your existing pattern from before)
            //targetDirection = Vector3.zero;
            //targetSpeed = 0f;
        }
    }
}