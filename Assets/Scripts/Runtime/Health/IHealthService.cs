namespace Runtime.Health
{
    public interface IHealthService
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }

        bool IsDead { get; }

        void Damage(int amount);
        void Heal(int amount);
        void Reset();
    }
}