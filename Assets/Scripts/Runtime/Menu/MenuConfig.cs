using UnityEngine;

namespace Runtime.Menu
{
    [CreateAssetMenu( fileName = "MenuConfig", menuName = "Menu/Menu Config")]
    public class MenuConfig : ScriptableObject
    {
        [Header("Scenes")]
        [SerializeField] private string gameSceneName = "GameScene";

        public string GameSceneName => gameSceneName;
    }
}