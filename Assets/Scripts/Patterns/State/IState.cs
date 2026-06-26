/*
 * IState.cs
 * 
 * Defines the contract for a state within a state machine.
 * Each state receives a reference to its owner, allowing it to control behavior contextually
 */


namespace Patterns.State
{
    /// <summary>
    /// Defines the contract for a state used in a generic state machine.
    /// Each state operates on a context object of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the context or owner controlled by the state.</typeparam>
    public interface IState<T>
    {
        /// <summary>
        /// Called once when the state is entered. Used to initialize or prepare the state.
        /// </summary>
        /// <param name="owner">The object that owns the state machine.</param>
        void Enter(T owner);    

        /// <summary>
        /// Called every frame while the state is active. Contains the main logic of the state.
        /// </summary>
        /// <param name="owner">The object that owns the state machine.</param>
        void Execute(T owner);   

        /// <summary>
        /// Called once when the state is exited. Used to clean up or finalize the state.
        /// </summary>
        /// <param name="owner">The object that owns the state machine.</param>
        void Exit(T owner);     
    }
}


