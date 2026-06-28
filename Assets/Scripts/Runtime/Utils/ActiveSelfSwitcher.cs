using UnityEngine;

namespace Runtime.Utils
{
    public class ActiveSelfSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject obj1;
        [SerializeField] private GameObject obj2;

        private int currentActive = 0;

        public void Switch()
        {
            if (obj1 == null || obj2 == null)
                return;

            if (currentActive == 0) 
            {
                obj1.SetActive(false);
                obj2.SetActive(true);
            }
            else
            {
                obj1.SetActive(true);
                obj2.SetActive(false);
            }

            currentActive = currentActive == 1 ? 0 : 1;
        }
    }
}

