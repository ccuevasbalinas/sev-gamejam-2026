using Runtime.Enumerators;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Runtime.Character
{
    [RequireComponent(typeof(PlayerCharacter))]
    public class PlayerInputHandler : MonoBehaviour
    {
        private InputSystem_Actions inputActions;
        private InputSystem_Actions.PlayerActions input;
        private PlayerCharacter player;

        private void Awake()
        {
            player = GetComponent<PlayerCharacter>();
            inputActions = new InputSystem_Actions();
            input = inputActions.Player; // <-- property access, not "new"
        }

        private void OnEnable()
        {
            input.Enable();

            input.Jump.performed += OnJump;
            input.Slide.performed += OnSlidePerformed;
            input.Slide.canceled += OnSlideCanceled;
            input.Attack.performed += OnAttack;
            input.TransitionMirror.performed += OnTransitionMirror;
            input.TransitionPhysical.performed += OnTransitionPhysical;
        }

        private void OnDisable()
        {
            input.Jump.performed -= OnJump;
            input.Slide.performed -= OnSlidePerformed;
            input.Slide.canceled -= OnSlideCanceled;
            input.Attack.performed -= OnAttack;
            input.TransitionMirror.performed -= OnTransitionMirror;
            input.TransitionPhysical.performed -= OnTransitionPhysical;

            input.Disable();
        }

        private void OnJump(InputAction.CallbackContext context) => player.RequestJump();

        private void OnSlidePerformed(InputAction.CallbackContext context) => player.RequestSlide(true);
        private void OnSlideCanceled(InputAction.CallbackContext context) => player.RequestSlide(false);

        private void OnAttack(InputAction.CallbackContext context) => player.RequestAttack();

        private void OnTransitionMirror(InputAction.CallbackContext context) =>
            player.RequestSwitchDimension(DimensionTarget.Mirror);

        private void OnTransitionPhysical(InputAction.CallbackContext context) =>
            player.RequestSwitchDimension(DimensionTarget.Physical);
    }
}