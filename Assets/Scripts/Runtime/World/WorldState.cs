namespace Runtime.World
{
    public class WorldStateService : IWorldState
    {
        private readonly WorldStateConfig config;

        public DimensionType CurrentDimension { get; private set; }

        public bool IsPhysical => CurrentDimension == DimensionType.Physical;
        public bool IsMirror => CurrentDimension == DimensionType.Mirror;

        public WorldStateService(WorldStateConfig config)
        {
            this.config = config;
            CurrentDimension = config.InitialDimension;
        }

        public void SetDimension(DimensionType dimension)
        {
            if (CurrentDimension == dimension)
                return;

            CurrentDimension = dimension;
            config.WorldChangedEvent?.Raise();
        }

        public void ToggleDimension()
        {
            SetDimension(IsPhysical ? DimensionType.Mirror : DimensionType.Physical);
        }

        public void ResetDimension()
        {
            CurrentDimension = config.InitialDimension;
            config.WorldChangedEvent?.Raise();
        }
    }
}