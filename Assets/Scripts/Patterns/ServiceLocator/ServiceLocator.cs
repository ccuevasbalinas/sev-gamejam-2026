/*
 * ServiceLocator.cs
 * 
 * Implements the Service Locator pattern for managing service instances.
 * Allows registering, unregistering, and retrieving services by type.
 * Internally stores services in a dictionary keyed by their type.
 */

using System;
using System.Collections.Generic;
using UnityEngine;


namespace Patterns.ServiceLocator
{
    /// <summary>
    /// Provides a global access point for registering, retrieving, and removing services using the Service Locator pattern.
    /// Manages service instances internally via a type-to-instance mapping.
    /// </summary>
    public static class ServiceLocator
    {
        private static readonly Dictionary<Type, object> _services = new();

        /// <summary>
        /// Registers a service instance of type <typeparamref name="T"/> into the service locator.
        /// </summary>
        /// <typeparam name="T">The type of the service to register.</typeparam>
        /// <param name="service">The service instance to register.</param>
        /// <returns>True if registration succeeded; false if a service of this type is already registered.</returns>
        public static bool Register<T>(T service)
        {
            Type serviceType = typeof(T);

            if (_services.ContainsKey(serviceType))
            {
                Debug.LogWarning("Service of type " + serviceType + " already registered.");
                return false;
            }

            _services.Add(serviceType, service);

            return true;
        }

        /// <summary>
        /// Unregisters the service of type <typeparamref name="T"/> from the service locator.
        /// </summary>
        /// <typeparam name="T">The type of the service to unregister.</typeparam>
        /// <returns>True if the service was successfully unregistered; false if no service of this type was registered.</returns>
        public static bool Unregister<T>()
        {
            Type serviceType = typeof(T);

            if (!_services.ContainsKey(serviceType))
            {
                Debug.LogWarning("Service of type " + serviceType + " is not registered.");
                return false;
            }

            _services.Remove(serviceType);

            return true;
        }

        /// <summary>
        /// Retrieves a registered service instance by the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type of the service.</typeparam>
        /// <param name="type">The type key used to look up the service.</param>
        /// <returns>The service instance cast to type <typeparamref name="T"/> if found; otherwise, default value of <typeparamref name="T"/>.</returns>
        public static T GetService<T>(Type type)
        {
            Type serviceType = typeof(T);

            if (!_services.ContainsKey(serviceType))
            {
                Debug.LogWarning("Service of type " + serviceType + " not found.");
                return default;
            }

            return (T)_services[serviceType];
        }

        /// <summary>
        /// Retrieves a registered service instance by the specified type.
        /// </summary>
        /// <typeparam name="T">The expected type of the service.</typeparam>
        /// <param name="type">The type key used to look up the service.</param>
        /// <returns>
        /// The service instance cast to type <typeparamref name="T"/> if found;
        /// otherwise, the default value of <typeparamref name="T"/>.
        /// </returns>
        public static T Get<T>()
        {
            return GetService<T>(typeof(T));
        }
    }
}