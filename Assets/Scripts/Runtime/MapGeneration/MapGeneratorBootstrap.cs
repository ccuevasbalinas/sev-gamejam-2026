using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.GameFlow;

namespace Runtime.MapGeneration
{
    public class MapGeneratorBootstrap : MonoBehaviour, IResettableGameSystem
    {
        [Header("Config")]
        [SerializeField] private MapGenerationConfig config;

        [Header("Scene References")]
        [SerializeField] private Transform segmentsParent;
        [SerializeField] private Transform firstSpawnPoint;

        private IMapGenerator MapGenerator;

        private void Awake()
        {
            MapGenerator = new MapGenerator(config, segmentsParent, firstSpawnPoint);

            ServiceLocator.Register<IMapGenerator>(MapGenerator);
        }

        public void ResetSystem()
        {
            MapGenerator.ResetMap();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<IMapGenerator>();
        }
    }
}