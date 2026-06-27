using UnityEngine;

namespace Runtime.Pickups
{
    public interface IPickupEffect
    {
        PickupType Type { get; }
        void Apply(GameObject player);
    }
}