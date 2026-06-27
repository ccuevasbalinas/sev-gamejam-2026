using UnityEngine;

namespace Runtime.Pickups
{
    [CreateAssetMenu(fileName = "PickupSpawnConfig", menuName = "Pickups/Spawn Config")]
    public class PickupSpawnConfig : ScriptableObject
    {
        [SerializeField] private PickupObject[] pickupPrefabs;
        [Range(0f, 1f)]
        [SerializeField] private float spawnChance = 0.5f;

        public PickupObject[] PickupPrefabs => pickupPrefabs;
        public float SpawnChance => spawnChance;
    }
}