using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.MapGeneration
{
    public class MapAdvanceTrigger : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";

        private bool triggered;

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;

            if (!other.CompareTag(playerTag))
                return;

            triggered = true;

            IMapGenerator generator = ServiceLocator.Get<IMapGenerator>();

            generator?.SpawnSegment();
        }

        private void OnEnable()
        {
            triggered = false;
        }
    }
}