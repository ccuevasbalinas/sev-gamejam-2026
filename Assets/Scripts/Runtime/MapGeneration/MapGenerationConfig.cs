using UnityEngine;

namespace Runtime.MapGeneration
{
    [CreateAssetMenu(fileName = "MapGenerationConfig", menuName = "Map Generation/Config")]
    public class MapGenerationConfig : ScriptableObject
    {
        [Header("Segments")]
        public MapSegment[] segmentPrefabs;

        [Header("Pooling")]
        public int initialPoolSizePerSegment = 3;

        [Header("Generation")]
        public int initialSegments = 6;
        public int maxActiveSegments = 8;
    }
}