using Runtime.World;

namespace Runtime.Health
{
    public interface IPlayerHealthService
    {
        int PhysicalHealth { get; }
        int MirrorHealth { get; }

        int MaxPhysicalHealth { get; }
        int MaxMirrorHealth { get; }

        bool IsDead { get; }

        void Damage(DimensionTarget target, int amount);
        void Heal(DimensionTarget target, int amount);
        void ResetHealth();
    }
}