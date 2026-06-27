using UnityEngine;

namespace Runtime.Pickups
{
    public class PickupSpawner : MonoBehaviour
    {
        [SerializeField] private PickupSpawnConfig config;
        [SerializeField] private Transform parent;

        private PickupSpawnPoint[] spawnPoints;
        private PickupObject currentPickup;

        private void Awake()
        {
            spawnPoints = GetComponentsInChildren<PickupSpawnPoint>(true);
        }

        private void OnEnable()
        {
            SpawnRandomPickup();
        }

        private void OnDisable()
        {
            if (currentPickup != null)
                currentPickup.gameObject.SetActive(false);
        }

        private void SpawnRandomPickup()
        {
            ClearCurrentPickup();

            if (config == null || config.PickupPrefabs.Length == 0)
                return;

            if (spawnPoints == null || spawnPoints.Length == 0)
                return;

            if (Random.value > config.SpawnChance)
                return;

            PickupSpawnPoint spawnPoint =
                spawnPoints[Random.Range(0, spawnPoints.Length)];

            PickupObject prefab =
                config.PickupPrefabs[Random.Range(0, config.PickupPrefabs.Length)];

            currentPickup = Instantiate(
                prefab,
                spawnPoint.transform.position,
                spawnPoint.transform.rotation,
                parent != null ? parent : transform
            );
        }

        private void ClearCurrentPickup()
        {
            if (currentPickup == null)
                return;

            Destroy(currentPickup.gameObject);
            currentPickup = null;
        }
    }
}