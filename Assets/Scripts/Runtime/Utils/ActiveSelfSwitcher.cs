using UnityEngine;

namespace Runtime.Utils
{
    public class ActiveSelfSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject obj1;
        [SerializeField] private GameObject obj2;

        public void Switch()
        {
            if (obj1 == null || obj2 == null)
                return;

            obj1.SetActive(!obj2.activeSelf);
            obj2.SetActive(!obj1.activeSelf);
        }
    }
}

