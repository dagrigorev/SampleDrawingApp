using JetBrains.Annotations;
using System;

namespace SampleDrawing.IoC
{
    /// <summary>
    /// Stupid IoC container interface
    /// </summary>
    public interface IServiceContainer : IServiceProvider
    {
        /// <summary>
        /// Registers new service with default constructor
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        void RegisterService<I, T>() where T : new();

        /// <summary>
        /// Registers new service with existed instance
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegsiterService<I, T>([ItemNotNull] T instance);

        /// <summary>
        /// Registers new service with existed instance
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="instance"></param>
        void RegsiterService<T>([ItemNotNull] T instance);

        /// <summary>
        /// Gets previously registered service
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        [NotNull]
        T GetService<T>();
    }
}
