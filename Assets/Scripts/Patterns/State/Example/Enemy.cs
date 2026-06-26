using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Patterns.State
{
    public class Enemy : MonoBehaviour
    {
        public StateMachine<Enemy> StateMachine { get; private set; }
        public string Name => gameObject.name; // Just for logging

        private void Start()
        {
            // Initialize the state machine with this enemy
            StateMachine = new StateMachine<Enemy>(this);
            StateMachine.ChangeState(new PatrolState());
        }

        private void Update()
        {
            // Let the state machine update the current state
            StateMachine.Update();

            // Press Space to simulate "seeing the player"
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log($"{Name} spots the player! Switching to ATTACK state.");
                StateMachine.ChangeState(new AttackState());
            }
        }

        // Example placeholder for movement
        public void MoveForward()
        {
            Debug.Log($"{Name} moves forward.");
        }

        // Example placeholder for animation trigger
        public void SetAnimation(string animName)
        {
            Debug.Log($"{Name} sets animation to {animName}.");
        }
    }
}


