using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SampleDrawing.Renderers
{
    public abstract class PrimitivesRenderer : DirectCanvasRenderer
    {
        private readonly Canvas _renderCanvas;

        public Canvas RenderCanvas => _renderCanvas;

        public PrimitivesRenderer(Canvas canvas) : base(canvas)
        {
            _renderCanvas = canvas;
        }

        protected override void Render()
        {
            if (PrimitivesCount <= 0)
                throw new ArgumentException(nameof(PrimitivesCount));

            _renderCanvas.Children.Clear();
            _renderCanvas.Background = new SolidColorBrush(GetColorFromString(DefaultColorCode));

            var drawingVisual = new DrawingVisual();
            var drawingContext = drawingVisual.RenderOpen();

            for (var i = 0; i < PrimitivesCount; i++)
            {
                RenderPrimitive(drawingContext);
            }

            drawingContext.Close();

            // Render context on bitmap
            var bmp = new RenderTargetBitmap(
                pixelWidth: (int)_renderCanvas.ActualWidth,
                pixelHeight: (int)_renderCanvas.ActualHeight,
                dpiX: 0, dpiY: 0, pixelFormat: PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            // Create image control from bitmap
            Image image = new Image();
            image.Source = bmp;

            // Add bitmap to canvas
            _renderCanvas.Children.Add(image);

            Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(() => { }));
        }

        public virtual void RenderPrimitive(DrawingContext drawingContext) { }
    }
}
