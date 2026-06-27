using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    public class ScoreMultiplierPickupEffect : IPickupEffect
    {
        private readonly float duration;

        public PickupType Type => PickupType.ScoreMultiplier;
        public bool IsTimedEffect => true;

        public ScoreMultiplierPickupEffect(float duration)
        {
            this.duration = duration;
        }

        public void Apply(GameObject player)
        {
            if (!ServiceLocator.Get<IPickupEffectHandler>().TryStartEffect(duration))
                return;

            // TODO: Activar x2 de puntuación durante duration segundos.
        }
    }
}