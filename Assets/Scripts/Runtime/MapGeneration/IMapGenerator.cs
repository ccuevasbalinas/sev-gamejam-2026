namespace Runtime.MapGeneration
{
    public interface IMapGenerator
    {
        void GenerateInitialMap();
        void SpawnSegment();
        void ResetMap();
        void Clear();
    }
}