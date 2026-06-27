namespace Runtime.GameFlow
{
    public class GameStatistics
    {
        public int Score { get; }
        public int Coins { get; }
        public float Time { get; }

        public GameStatistics(int score, int coins, float time)
        {
            Score = score;
            Coins = coins;
            Time = time;
        }
    }
}