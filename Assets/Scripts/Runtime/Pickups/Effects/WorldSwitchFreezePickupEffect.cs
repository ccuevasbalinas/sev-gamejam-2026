using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    public class WorldSwitchFreezePickupEffect : IPickupEffect
    {
        private readonly float duration;

        public PickupType Type => PickupType.WorldSwitchFreeze;
        public bool IsTimedEffect => true;

        public WorldSwitchFreezePickupEffect(float duration)
        {
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            if (!ServiceLocator.Get<IPickupEffectHandler>().TryStartEffect(duration))
                return;

            // TODO: Bloquear cambio de dimensión durante duration segundos.
        }
    }
}