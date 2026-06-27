using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    [RequireComponent(typeof(Collider))]
    public class PickupObject : MonoBehaviour
    {
        [SerializeField] private PickupConfig config;

        public PickupConfig Config => config;

        private bool collected;

        private void OnEnable()
        {
            collected = false;
        }

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (collected)
                return;

            if (!other.CompareTag("Player"))
                return;

            IPickupCollector collector = ServiceLocator.Get<IPickupCollector>();

            if (collector == null)
                return;

            collected = true;
            collector.Collect(this);

            gameObject.SetActive(false);
        }
    }
}