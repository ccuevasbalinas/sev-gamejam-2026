namespace Runtime.GameFlow
{
    public interface IGameManager
    {
        GameState CurrentState { get; }
        GameStatistics LastStatistics { get; }

        void GoToMainMenu();
        void StartGame();
        void FinishGame();
        void RestartGame();
        void QuitGame();

        void Tick(float deltaTime);
    }
}