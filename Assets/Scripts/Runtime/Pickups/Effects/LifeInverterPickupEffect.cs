using UnityEngine;

namespace Runtime.Pickups
{
    public class LifeInverterPickupEffect : IPickupEffect
    {
        public PickupType Type => PickupType.LifeInverter;
        public bool IsTimedEffect => false;

        public void Apply(GameObject player)
        {
            // TODO: Intercambiar vida actual Physical <-> Mirror.
        }
    }
}