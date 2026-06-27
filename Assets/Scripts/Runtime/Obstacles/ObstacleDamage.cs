using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.Health;
using Runtime.GameFlow;

namespace Runtime.Obstacles
{
    [RequireComponent(typeof(Collider))]
    public class ObstacleDamage : MonoBehaviour
    {
        [SerializeField] private int damage = 1;
        [SerializeField] private string playerTag = "Player";

        private bool hasHit;

        private void OnEnable()
        {
            hasHit = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (hasHit)
                return;

            if (!other.CompareTag(playerTag))
                return;

            IGameManagerService gameManager = ServiceLocator.Get<IGameManagerService>();

            if (gameManager == null ||
                gameManager.CurrentState != GameState.Playing)
                return;

            hasHit = true;

            ServiceLocator.Get<IHealthService>()?.Damage(damage);
        }

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }
    }
}