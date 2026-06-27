using UnityEngine;

namespace Runtime.Pickups
{
    public class PickupCollectorService : IPickupCollector
    {
        private readonly GameObject player;

        public PickupCollectorService(GameObject player)
        {
            this.player = player;
        }

        public void Collect(PickupObject pickup)
        {
            if (pickup == null || pickup.Config == null)
                return;

            IPickupEffect effect = PickupEffectFactory.Create(pickup.Config);

            effect.Apply(player);
        }
    }
}