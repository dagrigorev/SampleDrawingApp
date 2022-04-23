using System.Windows.Controls;
using System.Windows.Media;

namespace SampleDrawing.Renderers.PrimitiveRenderers
{
    public class CirclesRenderer : PrimitivesRenderer
    {
        public CirclesRenderer(Canvas canvas) : base(canvas)
        {
        }

        public override void RenderPrimitive(DrawingContext drawingContext)
        {
            var randomCenterPoint = GetRandomPoint(RenderCanvas.ActualWidth, RenderCanvas.ActualHeight);
            var randomRadius = (30 + PrimitiveRandomizer.Next()) % 50;
            var randomColor = GetRandomColor();
            var randomBrush = new SolidColorBrush(randomColor);

            drawingContext.DrawEllipse(
                randomBrush,
                new Pen(randomBrush, PrimitiveRandomizer.Next(1, 5)),
                randomCenterPoint, randomRadius, randomRadius);
        }
    }
}
