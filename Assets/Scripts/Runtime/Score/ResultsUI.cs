using TMPro;
using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.GameFlow;

namespace Runtime.Score
{
    public class ResultsUI : MonoBehaviour
    {
        [Header("Texts")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text coinsText;
        [SerializeField] private TMP_Text timeText;

        private IGameManagerService gameManager;

        private void Start()
        {
            gameManager = ServiceLocator.Get<IGameManagerService>();

            ShowResults();
        }

        private void ShowResults()
        {
            if (gameManager == null || gameManager.LastStatistics == null)
                return;

            GameStatistics stats = gameManager.LastStatistics;

            scoreText.text = $"Score: {stats.Score}";
            coinsText.text = $"Coins: {stats.Coins}";
            timeText.text = $"Time: {stats.Time:F2}s";
        }

        public void Restart()
        {
            gameManager?.RestartGame();
        }

        public void MainMenu()
        {
            gameManager?.GoToMainMenu();
        }

        public void Exit()
        {
            gameManager?.QuitGame();
        }
    }
}