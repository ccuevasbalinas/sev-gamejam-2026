using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    public class AttackFreezePickupEffect : IPickupEffect
    {
        private readonly float duration;

        public PickupType Type => PickupType.AttackFreeze;
        public bool IsTimedEffect => true;

        public AttackFreezePickupEffect(float duration)
        {
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            if (!ServiceLocator.Get<IPickupEffectHandler>().TryStartEffect(duration))
                return;

            // TODO: Bloquear ataque durante duration segundos.
        }
    }
}