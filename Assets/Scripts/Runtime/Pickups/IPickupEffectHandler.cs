namespace Runtime.Pickups
{
    public interface IPickupEffectHandler
    {
        bool HasActiveEffect { get; }

        bool TryStartEffect(float duration);
        void ClearEffect();
        void Tick(float deltaTime);
    }
}