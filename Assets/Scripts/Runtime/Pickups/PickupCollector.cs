using UnityEngine;

namespace Runtime.Pickups
{
    public class PickupCollector : IPickupCollector
    {
        private readonly GameObject player;

        public PickupCollector(GameObject player)
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