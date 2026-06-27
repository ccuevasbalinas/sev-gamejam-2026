using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.GameFlow;
using Runtime.World;

namespace Runtime.Health
{
    [RequireComponent(typeof(Collider))]
    public class ObstacleDamage : MonoBehaviour, IDamageSource
    {
        [Header("Damage")]
        [SerializeField] private DamageMode damageMode = DamageMode.NormalDamage;
        [SerializeField] private DimensionTarget targetDimension = DimensionTarget.Physical;
        [SerializeField] private int damage = 1;

        public int Damage => damage;

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

            hasHit = true;

            ApplyDamage();
        }

        public void ApplyDamage()
        {
            IPlayerHealth health = ServiceLocator.Get<IPlayerHealth>();

            if (health == null)
                return;

            switch (damageMode)
            {
                case DamageMode.NormalDamage:
                    ApplyNormalDamage(health);
                    break;

                case DamageMode.InstantKill:
                    ApplyInstantKill(health);
                    break;
            }
        }

        private void ApplyNormalDamage(IPlayerHealth health)
        {
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

        private void ApplyInstantKill(IPlayerHealth health)
        {
            switch (targetDimension)
            {
                case DimensionTarget.Physical:
                    health.Kill(DimensionType.Physical);
                    break;

                case DimensionTarget.Mirror:
                    health.Kill(DimensionType.Mirror);
                    break;

                case DimensionTarget.Both:
                    health.Kill(DimensionType.Physical);
                    health.Kill(DimensionType.Mirror);
                    break;
            }
        }
    }
}