using UnityEngine;

namespace Runtime.UI
{
    public class LifeBarUI : MonoBehaviour
    {
        [SerializeField] private GameObject life1;
        [SerializeField] private GameObject life2;
        [SerializeField] private GameObject life3;

        public void SetLife(int lifeValue)
        {
            if (life1 == null || life2 == null || life3 == null)
            {
                Debug.LogWarning("[LifeBarUI] Life Gameobjects not defined correctly!");
                return;
            }

            life1.SetActive(lifeValue >= 1);
            life2.SetActive(lifeValue >= 2);
            life3.SetActive(lifeValue == 3);
        }
    }
}

