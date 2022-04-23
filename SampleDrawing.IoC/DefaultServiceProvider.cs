using System;
using System.Collections.Generic;

namespace SampleDrawing.IoC
{
    /// <summary>
    /// Improvised stupid IoC container implementation.
    /// </summary>
    public class DefaultServiceProvider : IServiceContainer
    {
        private readonly IDictionary<Type, object> _depsStore;

        /// <summary>
        /// Default constructor.
        /// Initialize instances storage here.
        /// </summary>
        public DefaultServiceProvider()
        {
            _depsStore = new Dictionary<Type, object>();
        }

        /// <inheritdoc />
        public object GetService(Type serviceType)
        {
            if (!IsTypeAlreadyRegistered(serviceType))
                throw new KeyNotFoundException($"Type {serviceType.Name} was not registered");

            return _depsStore[serviceType];
        }

        /// <inheritdoc />
        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        /// <inheritdoc />
        public void RegisterService<I, T>() where T : new()
        {
            if (IsTypeAlreadyRegistered(typeof(T)))
                throw new ArgumentException($"Type {typeof(T).Name} has already registered");

            RegsiterService<I, T>(new T());
        }

        /// <inheritdoc />
        public void RegsiterService<I, T>(T instance)
        {
            if (IsTypeAlreadyRegistered(typeof(T)))
                throw new ArgumentException($"Type {typeof(T).Name} has already registered");

            _depsStore.Add(typeof(I), instance);
        }

        /// <inheritdoc />
        public void RegsiterService<T>(T instance)
        {
            if (IsTypeAlreadyRegistered(typeof(T)))
                throw new ArgumentException($"Type {typeof(T).Name} has already registered");

            _depsStore.Add(typeof(T), instance);
        }

        /// <inheritdoc />
        private bool IsTypeAlreadyRegistered(Type type) => _depsStore.ContainsKey(type);
    }
}
