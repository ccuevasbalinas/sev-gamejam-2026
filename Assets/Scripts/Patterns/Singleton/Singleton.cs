/*
 * Singleton.cs
 * 
 * A simple generic singleton pattern for plain C# classes.
 * Provides lazy initialization of a single shared instance of type T.
 * Suitable for non-MonoBehaviour classes that require a single global instance.
 * It does NOT prevent someone from creating other instances manually via the editor or scene loading.
 */


namespace Patterns.Singleton
{
    /// <summary>
    /// Implements a generic singleton pattern for any class.
    /// Ensures a single instance of <typeparamref name="T"/> is created and globally accessible.
    /// </summary>
    public class Singleton<T> where T : new()
    {
        private static T _instance;

        public static T Instance
        {
            get
            {
                if (_instance == null) 
                    _instance = new T();
                
                return _instance;
            }
        }
    }
}
