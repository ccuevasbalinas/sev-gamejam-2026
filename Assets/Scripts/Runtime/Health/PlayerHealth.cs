using Patterns.ServiceLocator;
using Runtime.GameFlow;
using Runtime.World;
using Patterns.ScriptableEvent;

namespace Runtime.Health
{
    public class PlayerHealth : IPlayerHealth
    {
        private readonly PlayerDualHealthConfig config;

        public int PhysicalHealth { get; private set; }
        public int MirrorHealth { get; private set; }

        public int MaxPhysicalHealth { get; }
        public int MaxMirrorHealth { get; }

        public bool IsDead => PhysicalHealth <= 0 || MirrorHealth <= 0;

        public PlayerHealth(PlayerDualHealthConfig config)
        {
            this.config = config;
            ResetHealth();
        }

        public void Damage(DimensionType dimension, int amount)
        {
            if (amount <= 0 || IsDead)
                return;

            switch (dimension)
            {
                case DimensionType.Physical:
                    PhysicalHealth -= amount;
                    if (PhysicalHealth < 0)
                        PhysicalHealth = 0;
                    break;

                case DimensionType.Mirror:
                    MirrorHealth -= amount;
                    if (MirrorHealth < 0)
                        MirrorHealth = 0;
                    break;
            }

            config.OnDamagedEvent?.Raise();

            CheckDeath();
        }

        public void Kill(DimensionType dimension)
        {
            if (IsDead)
                return;

            switch (dimension)
            {
                case DimensionType.Physical:
                    PhysicalHealth = 0;
                    break;

                case DimensionType.Mirror:
                    MirrorHealth = 0;
                    break;
            }

            config.OnDamagedEvent?.Raise();

            CheckDeath();
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

            config.OnHealedEvent?.Raise();
        }

        public void ResetHealth()
        {
            PhysicalHealth = MaxPhysicalHealth;
            MirrorHealth = MaxMirrorHealth;
        }

        private void CheckDeath()
        {
            if (!IsDead)
                return;

            config.OnDeathEvent?.Raise();

            ServiceLocator.Get<IGameManager>()?.FinishGame();
        }
    }
}