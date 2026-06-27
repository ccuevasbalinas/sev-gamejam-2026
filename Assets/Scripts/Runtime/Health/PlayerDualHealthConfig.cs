using UnityEngine;

namespace Runtime.Health
{
    [CreateAssetMenu(fileName = "PlayerDualHealthConfig", menuName = "Player/Dual Health Config")]
    public class PlayerDualHealthConfig : ScriptableObject
    {
        [Header("Physical")]
        [SerializeField] private int maxPhysicalHealth = 2;

        [Header("Mirror")]
        [SerializeField] private int maxMirrorHealth = 2;

        public int MaxPhysicalHealth => maxPhysicalHealth;
        public int MaxMirrorHealth => maxMirrorHealth;
    }
}