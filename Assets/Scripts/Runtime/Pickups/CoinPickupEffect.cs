using UnityEngine;
using Patterns.ServiceLocator;

using Runtime.Score;

namespace Runtime.Pickups
{
    public class CoinPickupEffect : IPickupEffect
    {
        private readonly int coinAmount;
        private readonly int scoreAmount;

        public PickupType Type => PickupType.Coin;

        public CoinPickupEffect(int coinAmount, int scoreAmount)
        {
            this.coinAmount = coinAmount;
            this.scoreAmount = scoreAmount;
        }

        public void Apply(GameObject player)
        {
            IScoreService scoreService = ServiceLocator.Get<IScoreService>();

            scoreService?.AddCoins(coinAmount);
            scoreService?.AddScore(scoreAmount);
        }
    }
}