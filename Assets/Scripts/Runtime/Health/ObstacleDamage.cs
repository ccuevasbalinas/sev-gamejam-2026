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
        [SerializeField] private DamageMode damageMode = DamageMode.NormalDamage;
        [SerializeField] private DimensionTarget targetDimension = DimensionTarget.Physical;
        [SerializeField] private int damage = 1;

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

            if (!other.CompareTag("Player"))
                return;

            IGameManager gameManager = ServiceLocator.Get<IGameManager>();

            if (gameManager == null || gameManager.CurrentState != GameState.Playing)
                return;

            IWorldState worldState = ServiceLocator.Get<IWorldState>();
            IPlayerHealth health = ServiceLocator.Get<IPlayerHealth>();

            if (worldState == null || health == null)
                return;

            DimensionType currentDimension = worldState.CurrentDimension;

            if (!CanDamageCurrentDimension(currentDimension))
                return;

            hasHit = true;

            ApplyDamage(health, currentDimension);
        }

        private bool CanDamageCurrentDimension(DimensionType currentDimension)
        {
            switch (targetDimension)
            {
                case DimensionTarget.Physical:
                    return currentDimension == DimensionType.Physical;

                case DimensionTarget.Mirror:
                    return currentDimension == DimensionType.Mirror;

                case DimensionTarget.Both:
                    return true;

                case DimensionTarget.None:
                default:
                    return false;
            }
        }

        private void ApplyDamage(IPlayerHealth health, DimensionType currentDimension)
        {
            switch (damageMode)
            {
                case DamageMode.NormalDamage:
                    health.Damage(currentDimension, damage);
                    break;

                case DamageMode.InstantKill:
                    health.Kill(currentDimension);
                    break;
            }
        }
    }
}