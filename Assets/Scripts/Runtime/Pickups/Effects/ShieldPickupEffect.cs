using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    public class ShieldPickupEffect : IPickupEffect
    {
        private readonly float duration;

        public PickupType Type => PickupType.Shield;
        public bool IsTimedEffect => true;

        public ShieldPickupEffect(float duration)
        {
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            if (!ServiceLocator.Get<IPickupEffectHandler>().TryStartEffect(duration))
                return;

            // TODO: Activar inmortalidad del jugador durante duration segundos.
        }
    }
}