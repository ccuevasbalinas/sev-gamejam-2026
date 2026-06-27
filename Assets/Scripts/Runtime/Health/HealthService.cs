using Patterns.ServiceLocator;
using Runtime.GameFlow;

namespace Runtime.Health
{
    public class HealthService : IHealthService
    {
        public int CurrentHealth { get; private set; }
        public int MaxHealth { get; private set; }

        public bool IsDead => CurrentHealth <= 0;

        public HealthService(int maxHealth)
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

                ServiceLocator.Get<IGameManagerService>()?.FinishGame();
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

        public void Reset()
        {
            CurrentHealth = MaxHealth;
        }
    }
}