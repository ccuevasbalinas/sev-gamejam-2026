using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    public class SpeedDownPickupEffect : IPickupEffect
    {
        private readonly float duration;

        public PickupType Type => PickupType.SpeedDown;
        public bool IsTimedEffect => true;

        public SpeedDownPickupEffect(float duration)
        {
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            if (!ServiceLocator.Get<IPickupEffectHandler>().TryStartEffect(duration))
                return;

            // TODO: Decrementar velocidad del jugador durante duration segundos.
        }
    }
}