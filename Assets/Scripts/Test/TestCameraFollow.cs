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
        private Quaternion baseRotation;

        private void Awake()
        {
            if (target != null)
                playerCharacter = target.GetComponentInParent<PlayerCharacter>()
                                  ?? target.GetComponent<PlayerCharacter>();

            // Fixed base orientation, facing the same way the rig was set up in the editor.
            baseRotation = transform.rotation;
        }

        private void LateUpdate()
        {
            if (target == null)
                return;

            // ---- Position: smoothly chase target + offset ----
            Vector3 desiredPosition = target.position + offset;
            transform.position = Vector3.Lerp(transform.position, desiredPosition, positionSmoothSpeed * Time.deltaTime);

            // ---- Rotation: smoothly blend toward a fixed tilt per dimension, no LookAt involved ----
            float targetRotationY = physicalRotationY;
            if (playerCharacter != null)
                targetRotationY = playerCharacter.CurrentDimension == DimensionType.Physical
                    ? physicalRotationY
                    : mirrorRotationY;

            Quaternion targetRotation = baseRotation * Quaternion.Euler(0f, targetRotationY, 0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSmoothSpeed * Time.deltaTime);
        }
    }
}