using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.GameFlow;

namespace Runtime.Triggers
{
    [RequireComponent(typeof(Collider))]
    public class GameOverTrigger : MonoBehaviour
    {
        [SerializeField] private string playerTag = "Player";

        private bool triggered;

        private void Reset()
        {
            Collider col = GetComponent<Collider>();
            col.isTrigger = true;
        }

        private void OnEnable()
        {
            triggered = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (triggered)
                return;

            if (!other.CompareTag(playerTag))
                return;

            IGameManager gameManager = ServiceLocator.Get<IGameManager>();

            if (gameManager == null)
                return;

            if (gameManager.CurrentState != GameState.Playing)
                return;

            triggered = true;
            gameManager.FinishGame();
        }
    }
}