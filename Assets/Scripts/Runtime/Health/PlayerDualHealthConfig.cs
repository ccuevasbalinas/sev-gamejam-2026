using Patterns.ScriptableEvent;
using UnityEngine;

namespace Runtime.Health
{
    [CreateAssetMenu(fileName = "PlayerDualHealthConfig", menuName = "Player/Dual Health Config")]
    public class PlayerDualHealthConfig : ScriptableObject
    {
        [Header("Physical")]
        [SerializeField] private int maxPhysicalHealth = 2;
        public int MaxPhysicalHealth => maxPhysicalHealth;

        [Header("Mirror")]
        [SerializeField] private int maxMirrorHealth = 2;
        public int MaxMirrorHealth => maxMirrorHealth;

        [Header("Events")]
        [SerializeField] private ScriptableEvent onDamagedEvent;
        [SerializeField] private ScriptableEvent onHealedEvent;
        [SerializeField] private ScriptableEvent onDeathEvent;

        public ScriptableEvent OnDamagedEvent => onDamagedEvent;
        public ScriptableEvent OnHealedEvent => onHealedEvent;
        public ScriptableEvent OnDeathEvent => onDeathEvent;
    }
}