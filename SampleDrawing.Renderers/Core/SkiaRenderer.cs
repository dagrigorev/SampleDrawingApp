using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SampleDrawing.Renderers.Core
{
    public class SkiaRenderer : DirectCanvasRenderer
    {
        private readonly Canvas _renderCanvas;

        public SkiaRenderer(Canvas canvas) : base(canvas)
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

            var imageInfo = new SKImageInfo(
                width: (int)_renderCanvas.ActualWidth,
                height: (int)_renderCanvas.ActualHeight,
                colorType: SKColorType.Rgba8888,
                alphaType: SKAlphaType.Premul);

            var surface = SKSurface.Create(imageInfo);
            var canvas = surface.Canvas;

            for (var i = 0; i < PrimitivesCount; i++)
            {
                var randomStartPoint = new SKPoint(PrimitiveRandomizer.Next((int)_renderCanvas.ActualWidth), 
                    PrimitiveRandomizer.Next((int)_renderCanvas.ActualHeight));
                var randomEndPoint = new SKPoint(PrimitiveRandomizer.Next((int)_renderCanvas.ActualWidth),
                    PrimitiveRandomizer.Next((int)_renderCanvas.ActualHeight));
                var randomColor = new SKColor(
                    red: (byte)PrimitiveRandomizer.Next(255),
                    green: (byte)PrimitiveRandomizer.Next(255),
                    blue: (byte)PrimitiveRandomizer.Next(255),
                    alpha: (byte)PrimitiveRandomizer.Next(255));

                var paint = new SKPaint
                {
                    Color = randomColor,
                    StrokeWidth = PrimitiveRandomizer.Next(1, 10),
                    IsAntialias = true,
                    Style = SKPaintStyle.Stroke
                };

                canvas.DrawLine(randomStartPoint, randomEndPoint, paint);
            }

            using (SKImage image = surface.Snapshot())
            using (SKBitmap bmp = SKBitmap.FromImage(image))
            {
                // _renderCanvas.Children.Add(bmp);
            }

            Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(() => { }));
        }
    }
}
