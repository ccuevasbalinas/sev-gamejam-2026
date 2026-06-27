using UnityEngine;

namespace Runtime.Pickups
{
    public class DestroyerPickupEffect : IPickupEffect
    {
        private readonly PickupConfig config;

        public PickupType Type => PickupType.Destroyer;
        public bool IsTimedEffect => false;

        public DestroyerPickupEffect(PickupConfig config)
        {
            this.config = config;
        }

        public void Apply(GameObject player)
        {
            config.DestroyWorldObjectsEvent?.Raise();
        }
    }
}