using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.GameFlow;
using Runtime.World;
using UnityEngine.Events;

namespace Runtime.Health
{
    [RequireComponent(typeof(Collider))]
    public class EnemyDamage : MonoBehaviour, IDamageSource
    {
        [Header("Damage")]
        [SerializeField] private DimensionTarget targetDimension = DimensionTarget.Both;
        [SerializeField] private int damage = 1;

        [Header("Events")]
        public UnityEvent OnEnemyDamagesPlayer;

        public int Damage => damage;

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            IGameManager gameManager = ServiceLocator.Get<IGameManager>();

            if (gameManager == null || gameManager.CurrentState != GameState.Playing)
                return;

            ApplyDamage();
        }

        public void ApplyDamage()
        {
            IPlayerHealth health = ServiceLocator.Get<IPlayerHealth>();

            if (health == null)
                return;

            OnEnemyDamagesPlayer?.Invoke();

            switch (targetDimension)
            {
                case DimensionTarget.Physical:
                    health.Damage(DimensionType.Physical, damage);
                    break;

                case DimensionTarget.Mirror:
                    health.Damage(DimensionType.Mirror, damage);
                    break;

                case DimensionTarget.Both:
                    health.Damage(DimensionType.Physical, damage);
                    health.Damage(DimensionType.Mirror, damage);
                    break;
            }
        }
    }
}