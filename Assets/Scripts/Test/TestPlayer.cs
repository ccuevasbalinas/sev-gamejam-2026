using UnityEngine;
using UnityEngine.InputSystem;

namespace Test.Player
{
    public class TestPlayer : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float forwardSpeed = 6f;
        [SerializeField] private float lateralSpeed = 5f;
        [SerializeField] private float lateralLimit = 2.5f;

        private void Update()
        {
            float horizontalInput = GetHorizontalInput();

            Vector3 movement = new Vector3(horizontalInput * lateralSpeed, 0f, forwardSpeed);

            transform.Translate(movement * Time.deltaTime, Space.World);

            Vector3 clampedPosition = transform.position;
            clampedPosition.x = Mathf.Clamp(clampedPosition.x, -lateralLimit, lateralLimit);

            transform.position = clampedPosition;
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