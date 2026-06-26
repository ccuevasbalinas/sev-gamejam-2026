using UnityEngine;
using Patterns.ServiceLocator;

namespace Runtime.Menu
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private MenuConfig menuConfig;

        public void Play()
        {
            ServiceLocator.Get<ISceneLoaderService>()?.LoadScene(menuConfig.GameSceneName);
        }

        public void Exit()
        {
            ServiceLocator.Get<IApplicationService>()?.Quit();
        }
    }
}