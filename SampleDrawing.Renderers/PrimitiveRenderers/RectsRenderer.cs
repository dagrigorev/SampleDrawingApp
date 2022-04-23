using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace SampleDrawing.Renderers.PrimitiveRenderers
{
    public class RectsRenderer : PrimitivesRenderer
    {
        public RectsRenderer(Canvas canvas) : base(canvas)
        {
        }

        public override void RenderPrimitive(DrawingContext drawingContext)
        {
            var randomStartPoint = GetRandomPoint(RenderCanvas.ActualWidth, RenderCanvas.ActualHeight);
            var randomEndPoint = GetRandomPoint(RenderCanvas.ActualWidth, RenderCanvas.ActualHeight);
            var randomColor = GetRandomColor();
            var randomBrush = new SolidColorBrush(randomColor);

            drawingContext.DrawRectangle(
                randomBrush,
                new Pen(randomBrush, PrimitiveRandomizer.Next(1, 20)),
                new Rect(randomStartPoint, randomEndPoint));
        }
    }
}
