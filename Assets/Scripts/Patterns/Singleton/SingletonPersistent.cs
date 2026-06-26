/*
 * SingletonPersistent.cs
 * 
 * A generic MonoBehaviour singleton base class that persists across scene loads.
 * Ensures that only one instance of a MonoBehaviour-derived class exists in the scene.
 * Automatically creates an instance if none exists, and enforces that only one instance survives.
 */

using UnityEngine;


namespace Patterns.Singleton
{
    /// <summary>
    /// A generic singleton base class that ensures only one instance of type <typeparamref name="T"/> exists in the scene.
    /// This instance persists across scene loads.
    /// </summary>
    public class SingletonPersistent<T> : MonoBehaviour  where T : Component
    {
        private static T _instance;
        public static T Instance
        {
            get
            {
                if (_instance == null)
                {
                    new GameObject(typeof(T).ToString()).AddComponent<T>();
                }

                return _instance;
            }
        }

        public virtual void Awake()
        {
            if (_instance == null)
            {
                _instance = this as T;
                Instance.transform.parent = null;
                DontDestroyOnLoad(this);
            }
            else
            {
                if (gameObject.GetComponents(typeof(Component)).Length > 1)
                    Destroy(this);
                else
                    Destroy(gameObject);
            }
        }
    }
}
