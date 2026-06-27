using UnityEngine;

namespace Runtime.Pickups
{
    [CreateAssetMenu(fileName = "PickupConfig", menuName = "Pickups/Pickup Config")]
    public class PickupConfig : ScriptableObject
    {
        [Header("Type")]
        [SerializeField] private PickupType pickupType;

        [Header("Values")]
        [SerializeField] private int scoreAmount = 10;
        [SerializeField] private int coinAmount = 1;

        public PickupType PickupType => pickupType;
        public int ScoreAmount => scoreAmount;
        public int CoinAmount => coinAmount;
    }
}