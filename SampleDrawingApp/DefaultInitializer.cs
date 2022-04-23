using SampleDrawing.IoC;
using SampleDrawing.Renderers;
using System.Windows.Controls;

namespace SampleDrawingApp
{
    /// <summary>
    /// Application default initializer.
    /// Uses IoC container for passing args.
    /// </summary>
    public class DefaultRendererInitializer : IApplicationInitializer
    {
        private readonly IServiceContainer _container;
        private readonly Canvas _canvas;

        public IServiceContainer Container => _container;

        public DefaultRendererInitializer(Canvas canvas)
        {
            _container = new DefaultServiceProvider();
            _canvas = canvas;
        }

        public void RegisterDependencies()
        {
            _container.RegsiterService<Canvas>(_canvas);
            _container.RegsiterService<AbstractRenderer>(new ContextRenderer(_canvas));
        }

        public static IApplicationInitializer CreateInitializer(Canvas canvas) => new DefaultRendererInitializer(canvas);

        public IApplicationInitializer Initialize()
        {
            RegisterDependencies();
            return this;
        }
    }
}
