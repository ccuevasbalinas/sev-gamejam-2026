/*
 * StateMachine.cs
 * 
 * Generic state machine implementation for managing the active state of an object.
 * Handles state transitions and delegates lifecycle calls (Enter, Execute, Exit)
 * to the currently active state.
 */


namespace Patterns.State
{
    /// <summary>
    /// A generic state machine that manages transitions and lifecycle for an object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the object that the state machine controls.</typeparam>
    public class StateMachine<T>
    {
        private T owner;
        public IState<T> CurrentState { get; private set; }

        public StateMachine(T owner)
        {
            this.owner = owner;
        }

        /// <summary>
        /// Transitions the state machine to a new state.
        /// </summary>
        /// <param name="newState">The new state to transition to.</param>
        public void ChangeState(IState<T> newState)
        {
            // Exit current state if it exists
            CurrentState?.Exit(owner);

            // Assign and enter the new state
            CurrentState = newState;
            CurrentState?.Enter(owner);
        }

        public void Update()
        {
            // Update the current state
            CurrentState?.Execute(owner);
        }
    }
}


