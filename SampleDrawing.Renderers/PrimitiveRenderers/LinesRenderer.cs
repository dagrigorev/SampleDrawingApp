using System.Windows.Controls;
using System.Windows.Media;

namespace SampleDrawing.Renderers.PrimitiveRenderers
{
    public class LinesRenderer : RandomPrimitivesRenderer
    {
        public LinesRenderer(Canvas canvas) : base(canvas)
        {
        }

        public override void RenderPrimitive(DrawingContext drawingContext)
        {
            var randomStartPoint = GetRandomPoint(RenderCanvas.ActualWidth, RenderCanvas.ActualHeight);
            var randomEndPoint = GetRandomPoint(RenderCanvas.ActualWidth, RenderCanvas.ActualHeight);
            var randomColor = GetRandomColor();
            var randomBrush = new SolidColorBrush(randomColor);

            drawingContext.DrawLine(
                new Pen(randomBrush, PrimitiveRandomizer.Next(1, 20)),
                randomStartPoint, randomEndPoint);
        }
    }
}
