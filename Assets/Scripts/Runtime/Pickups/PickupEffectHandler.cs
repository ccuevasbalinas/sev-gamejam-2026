namespace Runtime.Pickups
{
    public class PickupEffectHandler : IPickupEffectHandler
    {
        private float remainingTime;

        public bool HasActiveEffect => remainingTime > 0f;

        public bool TryStartEffect(float duration)
        {
            if (HasActiveEffect)
                return false;

            remainingTime = duration;
            return true;
        }

        public void ClearEffect()
        {
            remainingTime = 0f;
        }

        public void Tick(float deltaTime)
        {
            if (remainingTime <= 0f)
                return;

            remainingTime -= deltaTime;

            if (remainingTime < 0f)
                remainingTime = 0f;
        }
    }
}