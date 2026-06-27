namespace Runtime.GameFlow
{
    public interface IGameManagerService
    {
        GameState CurrentState { get; }
        GameStatistics LastStatistics { get; }

        void StartGame();
        void FinishGame();
        void RestartGame();
        void GoToMainMenu();
        void GoToResults();
        void QuitGame();
        void Tick(float deltaTime);
    }
}