using Patterns.ServiceLocator;
using Runtime.Health;
using Runtime.Menu;
using Runtime.Score;
using Runtime.Timer;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.GameFlow
{
    public class GameManagerService : IGameManagerService
    {
        private readonly IReadOnlyList<IResettableGameSystem> resettableSystems;

        public GameState CurrentState { get; private set; }
        public GameStatistics LastStatistics { get; private set; }

        public GameManagerService(IReadOnlyList<IResettableGameSystem> resettableSystems)
        {
            this.resettableSystems = resettableSystems;
            CurrentState = GameState.Boot;
        }

        public void GoToMainMenu()
        {
            CurrentState = GameState.MainMenu;
            Time.timeScale = 1f;
        }

        public void StartGame()
        {
            ResetGameplay();

            CurrentState = GameState.Playing;
            Time.timeScale = 1f;

            ServiceLocator.Get<IGameTimerService>()?.Start();
        }

        public void FinishGame()
        {
            if (CurrentState != GameState.Playing)
                return;

            CurrentState = GameState.Results;

            IGameTimerService timer = ServiceLocator.Get<IGameTimerService>();

            IScoreService score = ServiceLocator.Get<IScoreService>();

            timer?.Stop();

            LastStatistics = new GameStatistics(
                score != null ? score.TotalScore : 0,
                score != null ? score.TotalCoins : 0,
                timer != null ? timer.ElapsedTime : 0f
            );
        }

        public void RestartGame()
        {
            StartGame();
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

        private void ResetGameplay()
        {
            ServiceLocator.Get<IScoreService>()?.Reset();
            ServiceLocator.Get<IHealthService>()?.Reset();

            IGameTimerService timer = ServiceLocator.Get<IGameTimerService>();

            timer?.Reset();

            foreach (IResettableGameSystem system in resettableSystems)
            {
                system.ResetSystem();
            }   
        }
    }
}