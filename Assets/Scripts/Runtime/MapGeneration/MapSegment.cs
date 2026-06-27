using Runtime.Pickups;
using UnityEngine;

namespace Runtime.MapGeneration
{
    public class MapSegment : MonoBehaviour
    {
        [Header("Connection Points")]
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        public Transform StartPoint => startPoint;
        public Transform EndPoint => endPoint;

        private PickupObject[] pickups;

        private void Awake()
        {
            pickups = GetComponentsInChildren<PickupObject>(true);

            if (startPoint == null || endPoint == null) 
            {
                Debug.LogError("[MapSegment] Start/End points of segment not defined!");
                enabled = false;
            }
        }

        private void OnEnable()
        {
            ResetPickups();
        }

        public void PlaceAt(Transform targetPoint)
        {
            transform.position = targetPoint.position;
            transform.rotation = targetPoint.rotation;
        }

        private void ResetPickups()
        {
            if (pickups == null)
                return;

            foreach (PickupObject pickup in pickups)
            {
                pickup.gameObject.SetActive(true);
            }
        }
    }
}