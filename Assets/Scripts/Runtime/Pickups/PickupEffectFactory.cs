namespace Runtime.Pickups
{
    public static class PickupEffectFactory
    {
        public static IPickupEffect Create(PickupConfig config)
        {
            switch (config.PickupType)
            {
                case PickupType.Coin:
                    return new CoinPickupEffect(config.CoinAmount, config.ScoreAmount);

                case PickupType.Score:
                    return new ScorePickupEffect(config.ScoreAmount);

                default:
                    return new ScorePickupEffect(config.ScoreAmount);
            }
        }
    }
}