namespace Runtime.Health
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        bool IsDead { get; }

        void Damage(int amount);
        void Heal(int amount);
        void ResetHealth();
    }
}