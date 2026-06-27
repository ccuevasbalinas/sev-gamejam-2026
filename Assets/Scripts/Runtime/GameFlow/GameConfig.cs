using UnityEngine;

namespace Runtime.GameFlow
{
    [CreateAssetMenu(fileName = "GameConfig", menuName = "Game/Game Config")]
    public class GameConfig : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField] private string mainMenuSceneName = "MainMenu";
        [SerializeField] private string gameplaySceneName = "MapGenerationTest";
        [SerializeField] private string resultsSceneName = "Results";

        public string MainMenuSceneName => mainMenuSceneName;
        public string GameplaySceneName => gameplaySceneName;
        public string ResultsSceneName => resultsSceneName;
    }
}