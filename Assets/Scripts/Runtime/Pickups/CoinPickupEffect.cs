using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.Score;

namespace Runtime.Pickups
{
    public class CoinPickupEffect : IPickupEffect
    {
        private readonly int coinAmount;
        private readonly int scorePerCoin;

        public PickupType Type => PickupType.Coin;

        public CoinPickupEffect(int coinAmount, int scorePerCoin)
        {
            this.coinAmount = coinAmount;
            this.scorePerCoin = scorePerCoin;
        }

        public void Apply(GameObject player)
        {
            ServiceLocator.Get<IScoreService>()?.AddCoinWithScore(coinAmount, scorePerCoin);
        }
    }
}