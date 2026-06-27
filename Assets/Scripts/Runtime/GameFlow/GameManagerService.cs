using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.Score;
using Runtime.Timer;
using Runtime.Menu;

namespace Runtime.GameFlow
{
    public class GameManagerService : IGameManagerService
    {
        private readonly GameConfig config;

        public GameState CurrentState { get; private set; }
        public GameStatistics LastStatistics { get; private set; }

        public GameManagerService(GameConfig config)
        {
            this.config = config;
            CurrentState = GameState.None;
        }

        public void StartGame()
        {
            CurrentState = GameState.Playing;

            ServiceLocator.Get<IScoreService>()?.Reset();

            IGameTimerService timer = ServiceLocator.Get<IGameTimerService>();

            timer?.Reset();
            timer?.Start();
        }

        public void FinishGame()
        {
            CurrentState = GameState.GameOver;

            IGameTimerService timer =
                ServiceLocator.Get<IGameTimerService>();

            timer?.Stop();

            IScoreService score =
                ServiceLocator.Get<IScoreService>();

            LastStatistics = new GameStatistics(
                score != null ? score.TotalScore : 0,
                score != null ? score.TotalCoins : 0,
                timer != null ? timer.ElapsedTime : 0f
            );

            GoToResults();
        }

        public void RestartGame()
        {
            ServiceLocator.Get<ISceneLoaderService>()?.LoadScene(config.GameplaySceneName);
        }

        public void GoToMainMenu()
        {
            CurrentState = GameState.MainMenu;

            ServiceLocator.Get<ISceneLoaderService>()?.LoadScene(config.MainMenuSceneName);
        }

        public void GoToResults()
        {
            CurrentState = GameState.Results;

            ServiceLocator.Get<ISceneLoaderService>()?.LoadScene(config.ResultsSceneName);
        }

        public void QuitGame()
        {
            ServiceLocator.Get<IApplicationService>()?.Quit();
        }

        public void Tick(float deltaTime)
        {
            if (CurrentState != GameState.Playing)
                return;

            ServiceLocator.Get<IGameTimerService>()?.Tick(deltaTime);
        }
    }
}