
/*
 * ObjectPool.cs
 * 
 * A generic object pooling system for Unity Components.
 * 
 * This class manages a reusable collection of objects of type <T>, where T is a Unity Component. 
 * Unlike pooling raw GameObjects, this design centers around Component instances, allowing direct 
 * access to behavior scripts while still instantiating and reusing the full GameObject they belong to.
 * 
 * Unity supports instantiating Components by cloning their associated GameObjects and returning the 
 * specific Component from the new instance. This enables type-safe, behavior-focused pooling where 
 */

using System.Collections.Generic;
using UnityEngine;


namespace Patterns.ObjectPool
{
    /// <summary>
    /// A generic object pool for Unity Components.
    /// Manages the reuse of objects of type <typeparamref name="T"/> by instantiating their associated GameObjects as needed
    /// and recycling them to avoid frequent allocations and destruction. 
    /// </summary>
    public class ObjectPool<T> where T : Component
    {
        private T prefab;
        private Transform parent;        // Optional parent to organize pooled objects in the scene hierarchy
        private List<T> pool = new();    // Internal list to store pooled objects

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class with a specified prefab, initial pool size, and optional parent transform.
        /// Preloads the pool with inactive instances of the prefab.
        /// </summary>
        /// <param name="prefab">The component prefab to instantiate and pool.</param>
        /// <param name="initialSize">The number of instances to preload into the pool.</param>
        /// <param name="parent">Optional parent transform to organize pooled objects in the hierarchy.</param>
        public ObjectPool(T prefab, int initialSize, Transform parent = null)
        {
            this.prefab = prefab;
            this.parent = parent;

            // Pre-fill the pool with inactive objects
            for (int i = 0; i < initialSize; i++)
            {
                AddObjectToPool();
            }
        }

        /// <summary>
        /// Instantiates a new instance of the prefab, adds it to the pool, and returns the component of type <typeparamref name="T"/> from the instantiated GameObject.
        /// </summary>
        private T AddObjectToPool()
        {
            T obj = Object.Instantiate(prefab, parent);  // Instantiate under the optional parent
            obj.gameObject.SetActive(false);             // Disable it so it�s not active in the scene
            pool.Add(obj);                               // Add it to the pool
            return obj;                                  // Allows to expand pool dinamically on Get() if no object available
        }

        /// <summary>
        /// Retrieves an available instance of type <typeparamref name="T"/> from the pool.
        /// If no inactive instances are available, a new one is instantiated and added to the pool.
        /// The returned instance is activated and ready for use.
        /// </summary>
        public T Get()
        {
            foreach (var obj in pool)
            {
                // Look for an inactive (available) object
                if (!obj.gameObject.activeSelf)
                {
                    obj.gameObject.SetActive(true);
                    return obj;
                }
            }

            T newObj = AddObjectToPool();
            newObj.gameObject.SetActive(true);

            return newObj;
        }

        /// <summary>
        /// Returns the specified instance of type <typeparamref name="T"/> back to the pool by deactivating its GameObject.
        /// </summary>
        public void ReturnToPool(T obj)
        {
            if (obj == null)
                return;

            obj.gameObject.SetActive(false);
        }
    }
}


