using UnityEngine;

using Patterns.ServiceLocator;
using Runtime.World;
using Runtime.Health;

namespace Runtime.Character
{
    public class SyncMeshHealth : MonoBehaviour
    {
        [SerializeField] private GameObject headObject;
        [SerializeField] private GameObject chestObject;

        IWorldState worldState;
        IPlayerHealth playerHealth;

        private int currentPlayerHealth = 1;

        private void Start()
        {
            worldState = ServiceLocator.Get<IWorldState>();
            playerHealth = ServiceLocator.Get<IPlayerHealth>();
        }

        public void OnHealthChanged()
        {
            SyncMesh();
        }

        public void OnDimensionChanged()
        {
            SyncMesh();
        }

        private void SetHealth()
        {
            if (worldState.CurrentDimension == DimensionType.Physical)
                currentPlayerHealth = playerHealth.PhysicalHealth;
            else
                currentPlayerHealth = playerHealth.MirrorHealth;
        }

        private void SyncMesh()
        {
            SetHealth();

            if (headObject != null)
                headObject.SetActive(currentPlayerHealth == 3);

            if (chestObject != null)
                chestObject.SetActive(currentPlayerHealth >= 2);
        }
    }
}
