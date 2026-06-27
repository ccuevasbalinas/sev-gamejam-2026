using UnityEngine;
using Patterns.ScriptableEvent;

namespace Runtime.Pickups
{
    [CreateAssetMenu(fileName = "PickupConfig", menuName = "Pickups/Pickup Config")]
    public class PickupConfig : ScriptableObject
    {
        [Header("Type")]
        [SerializeField] private PickupType pickupType;

        [Header("Score / Coins")]
        [SerializeField] private int scoreAmount = 10;
        [SerializeField] private int coinAmount = 1;

        [Header("Timed Effects")]
        [SerializeField] private float duration = 5f;

        [Header("Destroyer")]
        [SerializeField] private ScriptableEvent destroyWorldObjectsEvent;

        public PickupType PickupType => pickupType;
        public int ScoreAmount => scoreAmount;
        public int CoinAmount => coinAmount;
        public float Duration => duration;
        public ScriptableEvent DestroyWorldObjectsEvent => destroyWorldObjectsEvent;
    }
}