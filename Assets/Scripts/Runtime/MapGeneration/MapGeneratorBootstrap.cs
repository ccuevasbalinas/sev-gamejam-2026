using UnityEngine;

using Patterns.ServiceLocator;

namespace Runtime.MapGeneration
{
    public class MapGeneratorBootstrap : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private MapGenerationConfig config;

        [Header("Scene References")]
        [SerializeField] private Transform segmentsParent;
        [SerializeField] private Transform firstSpawnPoint;

        private IMapGeneratorService mapGeneratorService;

        private void Awake()
        {
            if (config == null)
            {
                Debug.LogError("[MapGeneratorBootstrap] Map Configuration not defined!");
            }

            mapGeneratorService = new MapGeneratorService(config, segmentsParent, firstSpawnPoint);

            ServiceLocator.Register<IMapGeneratorService>(mapGeneratorService);
        }

        private void Start()
        {
            mapGeneratorService.GenerateInitialMap();
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<IMapGeneratorService>();
        }
    }
}