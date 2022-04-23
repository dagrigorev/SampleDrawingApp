using System;
using System.Collections.Generic;

namespace SampleDrawing.IoC
{
    public class DefaultServiceProvider : IServiceContainer
    {
        private readonly IDictionary<Type, object> _depsStore;

        public DefaultServiceProvider()
        {
            _depsStore = new Dictionary<Type, object>();
        }

        public object GetService(Type serviceType)
        {
            if (!IsTypeAlreadyRegistered(serviceType))
                throw new KeyNotFoundException($"Type {serviceType.Name} was not registered");

            return _depsStore[serviceType];
        }

        public T GetService<T>()
        {
            return (T)GetService(typeof(T));
        }

        public void RegisterService<I, T>() where T : new()
        {
            if (IsTypeAlreadyRegistered(typeof(T)))
                throw new ArgumentException($"Type {typeof(T).Name} has already registered");

            RegsiterService<I, T>(new T());
        }
        
        public void RegsiterService<I, T>(T instance)
        {
            if (IsTypeAlreadyRegistered(typeof(T)))
                throw new ArgumentException($"Type {typeof(T).Name} has already registered");

            _depsStore.Add(typeof(I), instance);
        }

        public void RegsiterService<T>(T instance)
        {
            if (IsTypeAlreadyRegistered(typeof(T)))
                throw new ArgumentException($"Type {typeof(T).Name} has already registered");

            _depsStore.Add(typeof(T), instance);
        }

        private bool IsTypeAlreadyRegistered(Type type) => _depsStore.ContainsKey(type);
    }
}
