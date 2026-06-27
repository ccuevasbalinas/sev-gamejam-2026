using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Components
{
    /// <summary>
    /// ICharacterMotor implementation using Unity's CharacterController component.
    /// Use for entities driven entirely by script logic, no physics forces needed (enemies).
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerMotor : MonoBehaviour, ICharacterMotor
    {
        [SerializeField] private float gravity = -9.81f;

        private CharacterController cc;
        private Vector3 velocity;

        public bool IsGrounded => cc.isGrounded;
        public Vector3 Velocity => velocity;

        private void Awake()
        {
            cc = GetComponent<CharacterController>();
        }

        public void Move(Vector3 direction, float speed)
        {
            Vector3 horizontal = direction.normalized * speed;
            velocity.x = horizontal.x;
            velocity.z = horizontal.z;
        }

        public void Jump(float jumpForce)
        {
            velocity.y = jumpForce;
        }

        public void Tick(float deltaTime)
        {
            if (cc.isGrounded && velocity.y < 0f)
                velocity.y = -2f; // keeps grounded check stable

            velocity.y += gravity * deltaTime;
            cc.Move(velocity * deltaTime);
        }

        public void SetHeightScale(float scale)
        {
            throw new System.NotImplementedException();
        }
    }
}