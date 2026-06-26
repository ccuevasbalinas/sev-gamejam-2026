namespace Runtime.MapGeneration
{
    public interface IMapGeneratorService
    {
        void GenerateInitialMap();
        void SpawnSegment();
        void Clear();
    }
}