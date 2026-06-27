using System.Collections.Generic;
using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.Score;
using Runtime.Timer;
using Runtime.Pickups;
using Runtime.Menu;
using Runtime.Health;

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
        [SerializeField] private PlayerHealthConfig playerHealthConfig;

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
            ServiceLocator.Register<IHealthService>(new HealthService(playerHealthConfig.MaxHealth));

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
            ServiceLocator.Unregister<IHealthService>();
        }
    }
}