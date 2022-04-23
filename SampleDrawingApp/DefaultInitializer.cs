using JetBrains.Annotations;
using SampleDrawing.IoC;
using SampleDrawing.Renderers;
using SampleDrawing.Renderers.PrimitiveRenderers;
using System;
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

        /// <inheritdoc />
        public IServiceContainer Container => _container;

        /// <summary>
        /// Parametrized constructor
        /// </summary>
        /// <param name="canvas"></param>
        /// <exception cref="ArgumentNullException" />
        public DefaultRendererInitializer([ItemNotNull] Canvas canvas)
        {
            if (canvas is null)
                throw new ArgumentNullException(nameof(canvas));

            _container = new DefaultServiceProvider();
            _canvas = canvas;
        }

        /// <inheritdoc />
        public void RegisterDependencies()
        {
            _container.RegsiterService<Canvas>(_canvas);
            _container.RegsiterService<AbstractRenderer>(new MixedPrimitivesRenderer(_canvas));
        }

        /// <summary>
        /// Creates new instance of default initializer
        /// </summary>
        /// <param name="canvas">Required argument</param>
        /// <returns>instance of <see cref="DefaultRendererInitializer"/></returns>
        /// <exception cref="ArgumentNullException" />
        [NotNull]
        public static IApplicationInitializer CreateInitializer([ItemNotNull] Canvas canvas) => new DefaultRendererInitializer(canvas);

        /// <inheritdoc />
        public IApplicationInitializer Initialize()
        {
            RegisterDependencies();
            return this;
        }
    }
}
