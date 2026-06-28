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

        [Header("Animation")]
        [SerializeField] private Animator animator;
        [SerializeField] private string healthChangedTrigger = "OnHit";
        [SerializeField] private float syncDelay = 0.5f;

        private IWorldState worldState;
        private IPlayerHealth playerHealth;

        private Coroutine syncCoroutine;

        private void Awake()
        {
            if (animator == null)
                animator = GetComponentInChildren<Animator>();
        }

        private void Start()
        {
            worldState = ServiceLocator.Get<IWorldState>();
            playerHealth = ServiceLocator.Get<IPlayerHealth>();

            SyncMeshWithHealth();
        }

        public void OnHealthChanged()
        {
            RequestDelayedSync();
        }

        public void OnDimensionChanged()
        {
            RequestDelayedSync();
        }

        private void RequestDelayedSync()
        {
            if (syncCoroutine != null)
                StopCoroutine(syncCoroutine);

            syncCoroutine = StartCoroutine(SyncAfterAnimation());
        }

        private IEnumerator SyncAfterAnimation()
        {
            if (animator != null && !string.IsNullOrEmpty(healthChangedTrigger))
                animator.SetTrigger(healthChangedTrigger);

            yield return new WaitForSeconds(syncDelay);

            SyncMeshWithHealth();

            syncCoroutine = null;
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