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

        private IMapGeneratorService mapGeneratorService;

        private void Awake()
        {
            mapGeneratorService = new MapGeneratorService(config, segmentsParent, firstSpawnPoint);

            ServiceLocator.Register<IMapGeneratorService>(mapGeneratorService);
        }

        public void ResetSystem()
        {
            mapGeneratorService.ResetMap();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<IMapGeneratorService>();
        }
    }
}