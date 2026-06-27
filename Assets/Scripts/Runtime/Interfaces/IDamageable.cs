namespace Runtime.Interfaces
{
    /// <summary>
    /// Contract for anything that can take damage and report alive/dead state.
    /// Damage-dealing code (projectiles, hazards, melee) targets this interface only —
    /// it never needs to know if it hit a Player, Enemy, or anything else.
    /// </summary>
    public interface IDamageable
    {
        bool IsAlive { get; }
        void TakeDamage(float amount);
    }
}