using Runtime.World;

namespace Runtime.Health
{
    public interface IPlayerHealth
    {
        int PhysicalHealth { get; }
        int MirrorHealth { get; }

        int MaxPhysicalHealth { get; }
        int MaxMirrorHealth { get; }

        bool IsDead { get; }

        void Damage(DimensionType dimension, int amount);
        void Kill(DimensionType dimension);
        void Heal(DimensionTarget target, int amount);
        void ResetHealth();
    }
}