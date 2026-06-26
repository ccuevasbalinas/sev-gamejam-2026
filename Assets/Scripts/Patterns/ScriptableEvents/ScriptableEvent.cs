/*
 * ScriptableEvent.cs
 * 
 * Defines a ScriptableObject-based event system that allows decoupled communication between 
 * game components. ScriptableEvents can be raised to notify registered listeners without 
 * direct references between sender and receiver.
 */

using System.Collections.Generic;
using UnityEngine;


namespace Patterns.ScriptableEvent
{
    /// <summary>
    /// Represents a reusable, data-driven event that can be raised and listened to across the project.
    /// </summary>
    [CreateAssetMenu(fileName = "NewEvent", menuName = "ScriptableEvents/New Event", order = 0)]
    public class ScriptableEvent : ScriptableObject
    {
        private readonly List<IScriptableEventListener> _listeners = new List<IScriptableEventListener>();

        /// <summary>
        /// Registers a listener to receive notifications when this event is raised.
        /// </summary>
        /// <param name="listener">The listener implementing <see cref="IScriptableEventListener"/> to register.</param>
        public void RegiterListener(IScriptableEventListener listener)
        {
            if (_listeners.Contains(listener))
                return;

            _listeners.Add(listener);
        }

        /// <summary>
        /// Unregisters a listener from the event, so it will no longer be notified when the event is raised.
        /// </summary>
        /// <param name="listener">The object implementing <see cref="IScriptableEventListener"/> to remove from the listeners list.</param>
        public void UnregisterListener(IScriptableEventListener listener)
        {
            _listeners.Remove(listener);
        }

        /// <summary>
        /// Raises the event, notifying all registered listeners.
        /// </summary>
        public void Raise()
        {
            var listeners = new List<IScriptableEventListener>(_listeners);

            foreach (var listener in listeners)
                listener.RaiseEvent();
        }
    }
}


