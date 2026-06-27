using Patterns.ServiceLocator;
using Runtime.GameFlow;
using Runtime.World;

namespace Runtime.Health
{
    public class PlayerHealthService : IPlayerHealthService
    {
        public int PhysicalHealth { get; private set; }
        public int MirrorHealth { get; private set; }

        public int MaxPhysicalHealth { get; }
        public int MaxMirrorHealth { get; }

        public bool IsDead => PhysicalHealth <= 0 && MirrorHealth <= 0;

        public PlayerHealthService(int maxPhysicalHealth, int maxMirrorHealth)
        {
            MaxPhysicalHealth = maxPhysicalHealth;
            MaxMirrorHealth = maxMirrorHealth;

            ResetHealth();
        }

        public void Damage(DimensionTarget target, int amount)
        {
            if (amount <= 0 || IsDead)
                return;

            switch (target)
            {
                case DimensionTarget.Physical:
                    PhysicalHealth -= amount;
                    if (PhysicalHealth < 0)
                        PhysicalHealth = 0;
                    break;

                case DimensionTarget.Mirror:
                    MirrorHealth -= amount;
                    if (MirrorHealth < 0)
                        MirrorHealth = 0;
                    break;

                case DimensionTarget.Both:
                    PhysicalHealth -= amount;
                    MirrorHealth -= amount;

                    if (PhysicalHealth < 0)
                        PhysicalHealth = 0;

                    if (MirrorHealth < 0)
                        MirrorHealth = 0;
                    break;
            }

            if (IsDead)
                ServiceLocator.Get<IGameManager>()?.FinishGame();
        }

        public void Heal(DimensionTarget target, int amount)
        {
            if (amount <= 0 || IsDead)
                return;

            switch (target)
            {
                case DimensionTarget.Physical:
                    PhysicalHealth += amount;
                    if (PhysicalHealth > MaxPhysicalHealth)
                        PhysicalHealth = MaxPhysicalHealth;
                    break;

                case DimensionTarget.Mirror:
                    MirrorHealth += amount;
                    if (MirrorHealth > MaxMirrorHealth)
                        MirrorHealth = MaxMirrorHealth;
                    break;

                case DimensionTarget.Both:
                    PhysicalHealth += amount;
                    MirrorHealth += amount;

                    if (PhysicalHealth > MaxPhysicalHealth)
                        PhysicalHealth = MaxPhysicalHealth;

                    if (MirrorHealth > MaxMirrorHealth)
                        MirrorHealth = MaxMirrorHealth;
                    break;
            }
        }

        public void ResetHealth()
        {
            PhysicalHealth = MaxPhysicalHealth;
            MirrorHealth = MaxMirrorHealth;
        }
    }
}