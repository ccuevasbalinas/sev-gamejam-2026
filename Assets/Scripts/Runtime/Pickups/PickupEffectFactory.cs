namespace Runtime.Pickups
{
    public static class PickupEffectFactory
    {
        public static IPickupEffect Create(PickupConfig config)
        {
            switch (config.PickupType)
            {
                case PickupType.Coin:
                    return new CoinPickupEffect(
                        config.CoinAmount,
                        config.ScoreAmount
                    );

                case PickupType.SpeedUp:
                    return new SpeedUpPickupEffect(config.Duration);

                case PickupType.SpeedDown:
                    return new SpeedDownPickupEffect(config.Duration);

                case PickupType.Shield:
                    return new ShieldPickupEffect(config.Duration);

                case PickupType.ScoreMultiplier:
                    return new ScoreMultiplierPickupEffect(config.Duration);

                case PickupType.Destroyer:
                    return new DestroyerPickupEffect(config);

                case PickupType.LifeInverter:
                    return new LifeInverterPickupEffect();

                case PickupType.WorldSwitchFreeze:
                    return new WorldSwitchFreezePickupEffect(config.Duration);

                case PickupType.AttackFreeze:
                    return new AttackFreezePickupEffect(config.Duration);

                default:
                    return new CoinPickupEffect(
                        config.CoinAmount,
                        config.ScoreAmount
                    );
            }
        }
    }
}