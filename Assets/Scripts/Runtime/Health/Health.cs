using Patterns.ServiceLocator;
using Runtime.GameFlow;

namespace Runtime.Health
{
    public class Health : IHealth
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

        public bool IsDead => CurrentHealth <= 0;

        public Health(int maxHealth)
        {
            MaxHealth = maxHealth;
            CurrentHealth = maxHealth;
        }

        public void Damage(int amount)
        {
            if (amount <= 0 || IsDead)
                return;

            CurrentHealth -= amount;

            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;

                ServiceLocator.Get<IGameManager>()?.FinishGame();
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
            CurrentHealth = MaxHealth;
        }
    }
}