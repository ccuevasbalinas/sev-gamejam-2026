using UnityEngine;

namespace Runtime.Utils
{
    public class ActiveSelfSwitcher : MonoBehaviour
    {
        [SerializeField] private GameObject obj;

        public void Switch()
        {
            if (obj == null)
                return;

            obj.SetActive(!obj.activeSelf);
        }
    }
}

