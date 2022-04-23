using System;

namespace SampleDrawing.IoC
{
    public interface IServiceContainer : IServiceProvider
    {
        void RegisterService<I, T>() where T : new();

        void RegsiterService<I, T>(T instance);

        void RegsiterService<T>(T instance);

        T GetService<T>();
    }
}
