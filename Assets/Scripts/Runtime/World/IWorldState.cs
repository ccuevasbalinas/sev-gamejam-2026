namespace Runtime.World
{
    public interface IWorldState
    {
        DimensionType CurrentDimension { get; }

        bool IsPhysical { get; }
        bool IsMirror { get; }

        void SetDimension(DimensionType dimension);
        void ToggleDimension();
        void ResetDimension();
    }
}