namespace Runtime.Health
{
    public interface IDamageSource
    {
        int Damage { get; }

        void ApplyDamage();
    }
}