using UnityEngine;

using Patterns.ServiceLocator;
using Runtime.World;
using Runtime.Health;

namespace Runtime.Character
{
    public class SyncMeshHealth : MonoBehaviour
    {
        [Header("Physical World")]
        [SerializeField] private GameObject headObjectPhysical;
        [SerializeField] private GameObject chestObjectPhysical;

        [Header("Mirror World")]
        [SerializeField] private GameObject headObjectMirror;
        [SerializeField] private GameObject chestObjectMirror;

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
            SyncMeshWithHealth();
        }

        public void OnDimensionChanged()
        {
            SyncMeshWithHealth();
        }

        private void SyncMeshWithHealth()
        {
            if (worldState.CurrentDimension == DimensionType.Physical)
            {
                currentPlayerHealth = playerHealth.PhysicalHealth;

                if (headObjectPhysical != null)
                    headObjectPhysical.SetActive(currentPlayerHealth == 3);

                if (chestObjectPhysical != null)
                    chestObjectPhysical.SetActive(currentPlayerHealth >= 2);
            } 
            else
            {
                currentPlayerHealth = playerHealth.MirrorHealth;

                if (headObjectMirror != null)
                    headObjectMirror.SetActive(currentPlayerHealth == 3);

                if (chestObjectMirror != null)
                    chestObjectMirror.SetActive(currentPlayerHealth >= 2);
            }
        }
    }
}
