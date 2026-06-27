using UnityEngine;
using UnityEngine.InputSystem;
using Runtime.GameFlow;
using Patterns.ServiceLocator;

namespace Test.Player
{
    public class TestPlayer : MonoBehaviour, IResettableGameSystem
    {
        [Header("Movement")]
        [SerializeField] private float forwardSpeed = 6f;
        [SerializeField] private float lateralSpeed = 5f;
        [SerializeField] private float lateralLimit = 2.5f;

        private Vector3 initialPosition;
        private Quaternion initialRotation;

        IGameManagerService gameManager;

        private void Awake()
        {
            initialPosition = transform.position;
            initialRotation = transform.rotation;
        }

        private void Start()
        {
            gameManager = ServiceLocator.Get<IGameManagerService>();
        }

        private void Update()
        {
            if (gameManager == null || gameManager.CurrentState != GameState.Playing)
                return;

            float horizontalInput = GetHorizontalInput();

            Vector3 movement = new Vector3(
                horizontalInput * lateralSpeed,
                0f,
                forwardSpeed
            );

            transform.Translate(movement * Time.deltaTime, Space.World);

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(
                clampedPosition.x,
                -lateralLimit,
                lateralLimit
            );

            transform.position = clampedPosition;
        }

        public void ResetSystem()
        {
            transform.position = initialPosition;
            transform.rotation = initialRotation;
            gameObject.SetActive(true);
        }

        private float GetHorizontalInput()
        {
            float input = 0f;

            Keyboard keyboard = Keyboard.current;

            if (keyboard == null)
                return input;

            if (keyboard.aKey.isPressed || keyboard.leftArrowKey.isPressed)
                input -= 1f;

            if (keyboard.dKey.isPressed || keyboard.rightArrowKey.isPressed)
                input += 1f;

            return input;
        }
    }
}