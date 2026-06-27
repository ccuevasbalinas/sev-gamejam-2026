using UnityEngine;

namespace Runtime.Health
{
    [CreateAssetMenu(fileName = "PlayerHealthConfig",menuName = "Player/Health Config")]
    public class PlayerHealthConfig : ScriptableObject
    {
        [SerializeField] private int maxHealth = 3;

        public int MaxHealth => maxHealth;
    }
}