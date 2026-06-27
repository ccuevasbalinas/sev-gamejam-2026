using UnityEngine;
using Patterns.ServiceLocator;

using Runtime.Score;

namespace Runtime.Pickups
{
    public class ScorePickupEffect : IPickupEffect
    {
        private readonly int scoreAmount;

        public PickupType Type => PickupType.Score;

        public ScorePickupEffect(int scoreAmount)
        {
            this.scoreAmount = scoreAmount;
        }

        public void Apply(GameObject player)
        {
            ServiceLocator.Get<IScoreService>()?.AddScore(scoreAmount);
        }
    }
}