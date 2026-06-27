using Patterns.ServiceLocator;
using Runtime.Health;
using Runtime.Menu;
using Runtime.Pickups;
using Runtime.Score;
using Runtime.Timer;
using Runtime.World;
using System.Collections.Generic;
using UnityEngine;

namespace Runtime.GameFlow
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private GameSceneConfig config;

        [Header("References")]
        [SerializeField] private GameObject player;

        [Header("Resettable Systems")]
        [SerializeField] private MonoBehaviour[] resettableSystems;

        [Header("Health")]
        [SerializeField] private PlayerDualHealthConfig playerHealthConfig;

        [Header("World")]
        [SerializeField] private WorldStateConfig worldStateConfig;

        private IGameManagerService gameManager;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            if (config.StartInMainMenu)
                gameManager.GoToMainMenu();
            else
                gameManager.StartGame();
        }

        private void Update()
        {
            gameManager?.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            UnregisterServices();
        }

        private void RegisterServices()
        {
            ServiceLocator.Register<IScoreService>(new ScoreService());
            ServiceLocator.Register<IGameTimerService>(new GameTimerService());
            ServiceLocator.Register<IApplicationService>(new ApplicationService());
            ServiceLocator.Register<IPickupCollector>(new PickupCollectorService(player));
            ServiceLocator.Register<IPlayerHealthService>(new PlayerHealthService(
                playerHealthConfig.MaxPhysicalHealth, playerHealthConfig.MaxMirrorHealth)); 
            ServiceLocator.Register<IWorldState>(new WorldStateService(worldStateConfig));

            List<IResettableGameSystem> systems = GetResettableSystems();

            gameManager = new GameManagerService(systems);

            ServiceLocator.Register<IGameManagerService>(gameManager);
        }

        private List<IResettableGameSystem> GetResettableSystems()
        {
            List<IResettableGameSystem> systems = new();

            foreach (MonoBehaviour behaviour in resettableSystems)
            {
                if (behaviour is IResettableGameSystem resettable)
                    systems.Add(resettable);
            }

            return systems;
        }

        private void UnregisterServices()
        {
            ServiceLocator.Unregister<IGameManagerService>();
            ServiceLocator.Unregister<IPickupCollector>();
            ServiceLocator.Unregister<IApplicationService>();
            ServiceLocator.Unregister<IGameTimerService>();
            ServiceLocator.Unregister<IScoreService>();
            ServiceLocator.Unregister<IPlayerHealthService>();
            ServiceLocator.Unregister<IWorldState>();
        }
    }
}