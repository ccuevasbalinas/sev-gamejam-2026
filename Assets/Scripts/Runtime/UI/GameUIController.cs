using Patterns.ServiceLocator;
using Runtime.GameFlow;
using Runtime.Health;
using Runtime.Score;
using Runtime.Timer;
using TMPro;
using UnityEngine;

namespace Runtime.UI
{
    public class GameUIController : MonoBehaviour
    {
        [Header("Panels")]
        [SerializeField] private GameObject mainMenuPanel;
        [SerializeField] private GameObject hudPanel;
        [SerializeField] private GameObject resultsPanel;

        [Header("HUD")]
        [SerializeField] private TMP_Text scoreText;
        [SerializeField] private TMP_Text healthText;
        [SerializeField] private TMP_Text timeText;

        [Header("Results")]
        [SerializeField] private TMP_Text finalScoreText;
        [SerializeField] private TMP_Text finalCoinsText;
        [SerializeField] private TMP_Text finalTimeText;

        private IGameManagerService gameManager;

        private GameState lastState;

        private void Start()
        {
            gameManager = ServiceLocator.Get<IGameManagerService>();
            lastState = GameState.Boot;

            RefreshPanels();
        }

        private void Update()
        {
            if (gameManager == null)
                return;

            UpdateHud();

            if (lastState != gameManager.CurrentState)
            {
                RefreshPanels();
                lastState = gameManager.CurrentState;
            }
        }

        private void RefreshPanels()
        {
            GameState state = gameManager.CurrentState;

            mainMenuPanel.SetActive(state == GameState.MainMenu);
            hudPanel.SetActive(state == GameState.Playing);
            resultsPanel.SetActive(state == GameState.Results);

            if (state == GameState.Results)
                UpdateResults();
        }

        private void UpdateHud()
        {
            IScoreService score = ServiceLocator.Get<IScoreService>();
            IGameTimerService timer = ServiceLocator.Get<IGameTimerService>();
            IHealthService health = ServiceLocator.Get<IHealthService>();

            if (score != null)
            {
                scoreText.text = $"Score: {score.TotalScore} | Coins: {score.TotalCoins}";
            }

            if (health != null)
            {
                healthText.text =
                    $"Health: {health.CurrentHealth}/{health.MaxHealth}";
            }

            if (timer != null)
            {
                timeText.text = $"Time: {timer.ElapsedTime:F1}";
            }
        }

        private void UpdateResults()
        {
            GameStatistics stats = gameManager.LastStatistics;

            if (stats == null)
                return;

            finalScoreText.text = $"Score: {stats.Score}";
            finalCoinsText.text = $"Coins: {stats.Coins}";
            finalTimeText.text = $"Time: {stats.Time:F1}s";
        }

        public void Play()
        {
            gameManager?.StartGame();
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