using UnityEngine;

namespace Runtime.Pickups
{
    public interface IPickupEffect
    {
        PickupType Type { get; }
        bool IsTimedEffect { get; }

        void Apply(GameObject player);
    }
}