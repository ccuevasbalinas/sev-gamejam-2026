using UnityEngine;
using Patterns.ServiceLocator;
using Runtime.Score;
using Runtime.Timer;
using Runtime.Pickups;
using Runtime.GameFlow;
using Runtime.Menu;

namespace Runtime.GameFlow
{
    public class GameBootstrap : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private GameConfig gameConfig;

        [Header("References")]
        [SerializeField] private GameObject player;

        private IGameManagerService gameManager;

        private void Awake()
        {
            ServiceLocator.Register<IScoreService>(new ScoreService());

            ServiceLocator.Register<IGameTimerService>(new GameTimerService());

            ServiceLocator.Register<IPickupCollector>(new PickupCollectorService(player));

            gameManager = new GameManagerService(gameConfig);

            ServiceLocator.Register<IGameManagerService>(gameManager);
        }

        private void Start()
        {
            gameManager.StartGame();
        }

        private void Update()
        {
            gameManager.Tick(Time.deltaTime);
        }

        private void OnDestroy()
        {
            ServiceLocator.Unregister<IGameManagerService>();
            ServiceLocator.Unregister<IPickupCollector>();
            ServiceLocator.Unregister<IGameTimerService>();
            ServiceLocator.Unregister<IScoreService>();
        }
    }
}