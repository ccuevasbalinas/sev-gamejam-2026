/*
 * IScriptableEventListener.cs
 * 
 * Defines the contract for an event listener used with ScriptableEvents.
 * Implementing classes handle event logic when the associated ScriptableEvent is raised,
 * enabling a decoupled and reusable event-driven architecture.
 */


namespace Patterns.ScriptableEvent
{
    /// <summary>
    /// Interface for objects that listen and respond to a ScriptableEvent.
    /// </summary>
    public interface IScriptableEventListener
    {
        /// <summary>
        /// Triggers the ScriptableEvent, notifying all registered listeners.
        /// Use this method to broadcast the event and invoke responses from subscribers.
        /// </summary>
        void RaiseEvent();
    }
}

