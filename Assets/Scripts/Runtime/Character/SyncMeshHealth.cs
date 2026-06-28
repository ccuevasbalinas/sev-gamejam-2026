using System.Collections;
using UnityEngine;

using Patterns.ServiceLocator;
using Runtime.World;
using Runtime.Health;

namespace Runtime.Character
{
    public class SyncMeshHealth : MonoBehaviour
    {
        [Header("Mesh parts")]
        [SerializeField] private GameObject headObject;
        [SerializeField] private GameObject chestObject;

        private IWorldState worldState;
        private IPlayerHealth playerHealth;


        private void Start()
        {
            worldState = ServiceLocator.Get<IWorldState>();
            playerHealth = ServiceLocator.Get<IPlayerHealth>();

            SyncMeshWithHealth();
        }

        public void OnHealthChanged()
        {
            SyncMeshWithHealth();
        }

        public void OnDimensionChanged()
        {
            SyncMeshWithHealth();
        }

        private void SyncMeshWithHealth()
        {
            if (worldState == null || playerHealth == null)
                return;

            int currentPlayerHealth =
                worldState.CurrentDimension == DimensionType.Physical
                    ? playerHealth.PhysicalHealth
                    : playerHealth.MirrorHealth;

            if (headObject != null)
                headObject.SetActive(currentPlayerHealth == 3);

            if (chestObject != null)
                chestObject.SetActive(currentPlayerHealth >= 2);
        }
    }
}