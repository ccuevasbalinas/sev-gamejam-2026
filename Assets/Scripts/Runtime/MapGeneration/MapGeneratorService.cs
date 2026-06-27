using System.Collections.Generic;
using UnityEngine;

using Patterns.ObjectPool;

namespace Runtime.MapGeneration
{
    public class MapGeneratorService : IMapGeneratorService
    {
        private readonly MapGenerationConfig config;
        private readonly Transform parent;
        private readonly Transform firstSpawnPoint;

        private readonly List<ObjectPool<MapSegment>> pools = new();
        private readonly List<MapSegment> activeSegments = new();

        private Transform nextSpawnPoint;

        public MapGeneratorService(MapGenerationConfig config, Transform parent, Transform firstSpawnPoint)
        {
            this.config = config;
            this.parent = parent;
            nextSpawnPoint = firstSpawnPoint;

            this.firstSpawnPoint = firstSpawnPoint;
            nextSpawnPoint = firstSpawnPoint;

            CreatePools();
        }

        private void CreatePools()
        {
            foreach (MapSegment prefab in config.segmentPrefabs)
            {
                ObjectPool<MapSegment> pool = new(prefab, config.initialPoolSizePerSegment, parent);
                pools.Add(pool);
            }
        }

        public void GenerateInitialMap()
        {
            for (int i = 0; i < config.initialSegments; i++)
                SpawnSegment();
        }

        public void SpawnSegment()
        {
            if (pools.Count == 0)
            {
                Debug.LogWarning("No map segment pools available.");
                return;
            }

            int randomIndex = Random.Range(0, pools.Count);
            MapSegment segment = pools[randomIndex].Get();

            segment.PlaceAt(nextSpawnPoint);

            activeSegments.Add(segment);
            nextSpawnPoint = segment.EndPoint;

            TrimOldSegments();
        }

        public void Clear()
        {
            foreach (MapSegment segment in activeSegments)
                segment.gameObject.SetActive(false);

            activeSegments.Clear();
        }

        public void ResetMap()
        {
            Clear();
            nextSpawnPoint = firstSpawnPoint;

            GenerateInitialMap();
        }

        private void TrimOldSegments()
        {
            while (activeSegments.Count > config.maxActiveSegments)
            {
                MapSegment oldestSegment = activeSegments[0];
                activeSegments.RemoveAt(0);

                oldestSegment.gameObject.SetActive(false);
            }
        }
    }
}