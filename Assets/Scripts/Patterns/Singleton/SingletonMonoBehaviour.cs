/*
 * SingletonMonoBehaviour.cs
 * 
 * A generic MonoBehaviour singleton base class for Unity.
 * Ensures that only one instance of a MonoBehaviour-derived class exists in the scene.
 * Automatically creates an instance if none exists, and enforces that only one instance survives.
 */

using UnityEngine;


namespace Patterns.Singleton
{
    /// <summary>
    /// Generic MonoBehaviour singleton pattern implementation for Unity.
    /// Guarantees a single instance of <typeparamref name="T"/> in the scene and  auto-generates it.
    /// </summary>
    /// <typeparam name="T">Type of the MonoBehaviour singleton.</typeparam>
    public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
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
                _instance = this as T;
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
