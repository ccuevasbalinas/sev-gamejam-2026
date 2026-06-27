using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Pickups
{
    [RequireComponent(typeof(Collider))]
    public class PickupObject : MonoBehaviour
    {
        [SerializeField] private PickupConfig config;

        public PickupConfig Config => config;

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;

            IPickupCollector collector = ServiceLocator.Get<IPickupCollector>();

            collector?.Collect(this);

            gameObject.SetActive(false);
        }
    }
}