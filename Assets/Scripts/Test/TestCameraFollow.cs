using UnityEngine;
using Runtime.Character;
using Runtime.World;

namespace Test
{
    public class TestCameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private Vector3 offset = new Vector3(0f, 5f, -8f);
        [SerializeField] private float positionSmoothSpeed = 5f;

        [Header("Dimension Rotation")]
        [SerializeField] private float physicalRotationY = -15f;
        [SerializeField] private float mirrorRotationY = 15f;
        [SerializeField] private float rotationSmoothSpeed = 5f;

        private PlayerCharacter playerCharacter;
        private float currentRotationY;

        private void Awake()
        {
            if (target != null)
                playerCharacter = target.GetComponentInParent<PlayerCharacter>()
                                  ?? target.GetComponent<PlayerCharacter>();

            currentRotationY = physicalRotationY;
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            float targetRotationY = physicalRotationY;
            if (playerCharacter != null)
                targetRotationY = playerCharacter.CurrentDimension == DimensionType.Physical
                    ? physicalRotationY
                    : mirrorRotationY;

            currentRotationY = Mathf.Lerp(currentRotationY, targetRotationY, rotationSmoothSpeed * Time.deltaTime);

            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed * Time.deltaTime);

            transform.LookAt(target);
            transform.rotation *= Quaternion.Euler(0f, currentRotationY, 0f);
        }
    }
}
