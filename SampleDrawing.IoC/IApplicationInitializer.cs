using System;

namespace SampleDrawing.IoC
{
    /// <summary>
    /// Application dependencies initializer
    /// </summary>
    public interface IApplicationInitializer
    {
        /// <summary>
        /// Registered services container
        /// </summary>
        IServiceContainer Container { get; }

        /// <summary>
        /// Initializes app
        /// </summary>
        /// <returns></returns>
        IApplicationInitializer Initialize();

        /// <summary>
        /// Registers deps
        /// </summary>
        void RegisterDependencies();
    }
}
