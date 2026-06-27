using System;
using Runtime.Interfaces;
using UnityEngine;

namespace Runtime.Character
{
    public class Character : MonoBehaviour
    {
        [Header("Stats")]
        [SerializeField] protected float moveSpeed = 5f;
        [SerializeField] protected float jumpForce = 6f;

        [Header("Components")]
        [SerializeField] protected Animator animator;

        protected ICharacterMotor motor;

        public Animator Anim => animator;
        public bool IsGrounded => motor != null && motor.IsGrounded;
        public Vector3 Velocity => motor != null ? motor.Velocity : Vector3.zero;
        
        public event Action OnLanded;
        public event Action OnJumped;

        private bool wasGroundedLastTick;

        protected virtual void Awake()
        {
            if (animator == null)
                animator = GetComponentInChildren<Animator>();
        }

        protected void SetMotor(ICharacterMotor newMotor) => motor = newMotor;

        protected virtual void Update()
        {
            if (motor == null) return;

            motor.Tick(Time.deltaTime);
            CheckGroundedTransition();
        }

        private void CheckGroundedTransition()
        {
            bool grounded = motor.IsGrounded;
            if (grounded && !wasGroundedLastTick)
                OnLanded?.Invoke();

            wasGroundedLastTick = grounded;
        }

        protected virtual void Move(Vector3 direction, float overrideSpeed) => motor?.Move(direction, overrideSpeed);

        protected virtual void Jump()
        {
            if (motor == null || !motor.IsGrounded) return;
            motor.Jump(jumpForce);
            OnJumped?.Invoke();
        }
    }
}