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
        [Header("References")]
        [SerializeField] private GameObject player;

        [Header("Resettable Systems")]
        [SerializeField] private MonoBehaviour[] resettableSystems;

        [Header("Health")]
        [SerializeField] private PlayerDualHealthConfig playerHealthConfig;

        [Header("World")]
        [SerializeField] private WorldStateConfig worldStateConfig;

        private IGameManager gameManager;

        private void Awake()
        {
            RegisterServices();
        }

        private void Start()
        {
            gameManager.GoToMainMenu();
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
            ServiceLocator.Register<IGameTimer>(new GameTimer());
            ServiceLocator.Register<IApplicationService>(new ApplicationService());
            ServiceLocator.Register<IPickupCollector>(new PickupCollector(player));
            ServiceLocator.Register<IPlayerHealth>(new PlayerHealth(
                playerHealthConfig.MaxPhysicalHealth, playerHealthConfig.MaxMirrorHealth)); 
            ServiceLocator.Register<IWorldState>(new WorldStateService(worldStateConfig));
            ServiceLocator.Register<IPickupEffectHandler>(new PickupEffectHandler());

            List<IResettableGameSystem> systems = GetResettableSystems();

            gameManager = new GameManager(systems);

            ServiceLocator.Register<IGameManager>(gameManager);
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
            ServiceLocator.Unregister<IGameManager>();
            ServiceLocator.Unregister<IPickupCollector>();
            ServiceLocator.Unregister<IApplicationService>();
            ServiceLocator.Unregister<IGameTimer>();
            ServiceLocator.Unregister<IScoreService>();
            ServiceLocator.Unregister<IPlayerHealth>();
            ServiceLocator.Unregister<IWorldState>();
        }
    }
}