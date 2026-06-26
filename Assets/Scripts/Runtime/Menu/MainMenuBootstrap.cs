using UnityEngine;

using Patterns.ServiceLocator;

namespace Runtime.Menu
{
    public class MenuBootstrap : MonoBehaviour
    {
        private void Awake()
        {
            ServiceLocator.Register<ISceneLoaderService>(new SceneLoaderService());
            ServiceLocator.Register<IApplicationService>(new ApplicationService());
        }
    }
}