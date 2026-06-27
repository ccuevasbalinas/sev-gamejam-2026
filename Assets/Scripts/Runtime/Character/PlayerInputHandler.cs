using UnityEngine;
using UnityEngine.InputSystem;

using Runtime.World;
using Patterns.ServiceLocator;
using Runtime.GameFlow;

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
            input = inputActions.Player;
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

        private bool CanProcessInput()
        {
            IGameManager gameManager =
                ServiceLocator.Get<IGameManager>();

            return gameManager != null &&
                   gameManager.CurrentState == GameState.Playing;
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (!CanProcessInput())
                return;

            player.RequestJump();
        }

        private void OnSlidePerformed(InputAction.CallbackContext context)
        {
            if (!CanProcessInput())
                return;

            player.RequestSlide(true);
        }

        private void OnSlideCanceled(InputAction.CallbackContext context)
        {
            if (!CanProcessInput())
                return;

            player.RequestSlide(false);
        }

        private void OnAttack(InputAction.CallbackContext context)
        {
            if (!CanProcessInput())
                return;

            player.RequestAttack();
        }

        private void OnTransitionMirror(InputAction.CallbackContext context)
        {
            if (!CanProcessInput())
                return;

            player.RequestSwitchDimension(DimensionType.Mirror);
        }

        private void OnTransitionPhysical(InputAction.CallbackContext context)
        {
            if (!CanProcessInput())
                return;

            player.RequestSwitchDimension(DimensionType.Physical);
        }
    }
}