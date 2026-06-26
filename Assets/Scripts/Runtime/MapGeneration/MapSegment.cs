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

        private void Awake()
        {
            if (startPoint == null || endPoint == null) 
            {
                Debug.LogError("[MapSegment] Start/End points of segment not defined!");
                enabled = false;
            }
        }

        public void PlaceAt(Transform targetPoint)
        {
            transform.position = targetPoint.position;
            transform.rotation = targetPoint.rotation;
        }
    }
}