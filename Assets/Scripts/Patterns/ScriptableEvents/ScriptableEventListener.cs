/*
 * ScriptableEventListener.cs
 * 
 * MonoBehaviour implementation of IScriptableEventListener that listens to ScriptableEvents
 * and invokes a response when the event is raised.
 * This script facilitates a decoupled event system where events are defined as ScriptableObjects,
 * and listeners can respond to those events without direct references to the event sender.
 */

using UnityEngine;
using UnityEngine.Events;


namespace Patterns.ScriptableEvent
{
    /// <summary>
    /// MonoBehaviour implementation of <see cref="IScriptableEventListener"/> that listens for a <see cref="ScriptableEvent"/> and invokes a response when the event is raised.
    /// </summary>
    public class ScriptableEventListener : MonoBehaviour, IScriptableEventListener
    {
        [SerializeField] private ScriptableEvent _scriptableEvent;
        [field: SerializeField] public UnityEvent OnEventRaised { get; private set; }


        private void OnEnable()
        {
            _scriptableEvent.RegiterListener(this);
        }

        private void OnDisable()
        {
            _scriptableEvent.UnregisterListener(this);
        }

        /// <summary>
        /// Triggers the ScriptableEvent, notifying all registered listeners.
        /// Use this method to broadcast the event and invoke responses from subscribers.
        /// </summary>
        public void RaiseEvent()
        {
            OnEventRaised.Invoke();
        }
    }
}


