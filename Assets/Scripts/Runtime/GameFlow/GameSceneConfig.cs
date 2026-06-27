using UnityEngine;

namespace Runtime.GameFlow
{
    [CreateAssetMenu(fileName = "GameSceneConfig", menuName = "Game/Game Scene Config")]
    public class GameSceneConfig : ScriptableObject
    {
        [Header("Gameplay")]
        [SerializeField] private bool startInMainMenu = true;

        public bool StartInMainMenu => startInMainMenu;
    }
}