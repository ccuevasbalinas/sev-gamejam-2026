using UnityEngine;

namespace Runtime.Score
{
    public class ScoreService : IScoreService
    {
        public int TotalScore { get; private set; }
        public int TotalCoins { get; private set; }

        public void AddScore(int amount)
        {
            if (amount <= 0)
                return;

            TotalScore += amount;
        }

        public void AddCoins(int amount)
        {
            if (amount <= 0)
                return;

            TotalCoins += amount;
        }

        public void Reset()
        {
            TotalScore = 0;
            TotalCoins = 0;
        }
    }
}