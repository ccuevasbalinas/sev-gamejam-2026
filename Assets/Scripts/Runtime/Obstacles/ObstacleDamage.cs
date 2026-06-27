using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.GameFlow;
using Runtime.World;

namespace Runtime.Health
{
    [RequireComponent(typeof(Collider))]
    public class ObstacleDamage : MonoBehaviour
    {
        [Header("Damage")]
        [SerializeField] private DimensionTarget targetDimension = DimensionTarget.Physical;
        [SerializeField] private int damage = 1;

        [Header("Collision")]
        [SerializeField] private string playerTag = "Player";

        private bool hasHit;

        private void OnEnable()
        {
            hasHit = false;
        }

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hasHit)
                return;

            if (!other.CompareTag(playerTag))
                return;

            IGameManager gameManager = ServiceLocator.Get<IGameManager>();

            if (gameManager == null || gameManager.CurrentState != GameState.Playing)
                return;

            hasHit = true;

            ServiceLocator.Get<IPlayerHealthService>()?.Damage(targetDimension, damage);
        }
    }
}