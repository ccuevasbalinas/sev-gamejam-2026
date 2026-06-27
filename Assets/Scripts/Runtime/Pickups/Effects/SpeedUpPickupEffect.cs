using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    public class SpeedUpPickupEffect : IPickupEffect
    {
        private readonly float duration;

        public PickupType Type => PickupType.SpeedUp;
        public bool IsTimedEffect => true;

        public SpeedUpPickupEffect(float duration)
        {
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            if (!ServiceLocator.Get<IPickupEffectHandler>().TryStartEffect(duration))
                return;

            // TODO: Incrementar velocidad del jugador durante duration segundos.
        }
    }
}