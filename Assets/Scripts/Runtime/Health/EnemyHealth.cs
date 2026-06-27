using UnityEngine;

namespace Runtime.Health
{
    public class EnemyHealth : MonoBehaviour, IHealth
    {
        private int maxHealth = 1;
        public int CurrentHealth { get; private set; }
        public int MaxHealth => maxHealth;
        public bool IsDead => CurrentHealth <= 0;

        private void Awake()
        {
            ResetHealth();
        }

        public void Damage(int amount)
        {
            if (amount <= 0 || IsDead)
                return;

            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Die();
            }
        }

        public void Heal(int amount)
        {
            if (amount <= 0 || IsDead)
                return;

            CurrentHealth += amount;

            if (CurrentHealth > MaxHealth)
                CurrentHealth = MaxHealth;
        }

        public void ResetHealth()
        {
            CurrentHealth = maxHealth;
        }

        private void Die()
        {
            gameObject.SetActive(false);
        }
    }
}