using UnityEngine;

namespace Runtime.Interfaces
{
    /// <summary>
    /// Contract for anything that physically moves a Character.
    /// Lets Character stay agnostic to whether movement is Rigidbody-based,
    /// CharacterController-based, or something else entirely.
    /// </summary>
    public interface ICharacterMotor
    {
        bool IsGrounded { get; }
        Vector3 Velocity { get; }

        /// <summary>Horizontal movement input, normalized direction (not multiplied by speed).</summary>
        void Move(Vector3 direction, float speed);

        void Jump(float jumpForce);

        /// <summary>Called every physics/movement tick by the owning Character.</summary>
        void Tick(float deltaTime);
        
        /// <summary>
        /// Sets the collider height as a fraction of its original height (1 = normal, 0.5 = half),
        /// keeping the bottom (feet) position fixed in world space.
        /// </summary>
        void SetHeightScale(float scale);
    }
}