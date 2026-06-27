namespace Runtime.Score
{
    public interface IScoreService
    {
        int TotalScore { get; }
        int TotalCoins { get; }

        void AddScore(int amount);
        void AddCoins(int amount);
        void AddCoinWithScore(int coinAmount, int scorePerCoin);
        void Reset();
    }
}