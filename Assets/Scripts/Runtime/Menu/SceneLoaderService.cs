using UnityEngine.SceneManagement;

namespace Runtime.Menu
{
    public class SceneLoaderService : ISceneLoaderService
    {
        public void LoadScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}