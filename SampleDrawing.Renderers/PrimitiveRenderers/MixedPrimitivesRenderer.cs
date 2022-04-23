using System.Windows.Controls;
using System.Windows.Media;

namespace SampleDrawing.Renderers.PrimitiveRenderers
{
    /// <summary>
    /// Mutiple primitives renderer
    /// </summary>
    public class MixedPrimitivesRenderer : RandomPrimitivesRenderer
    {
        private readonly RandomPrimitivesRenderer[] _primitivesRenderers;

        public MixedPrimitivesRenderer(Canvas canvas) : base(canvas)
        {
            _primitivesRenderers = new RandomPrimitivesRenderer[] {
                new CirclesRenderer(canvas),
                new RectsRenderer(canvas),
                new LinesRenderer(canvas)
            };
        }

        public override void RenderPrimitive(DrawingContext drawingContext)
        {
            var randomPrimitiveRendererIndex = PrimitiveRandomizer.Next() % _primitivesRenderers.Length;
            _primitivesRenderers[randomPrimitiveRendererIndex].RenderPrimitive(drawingContext);
        }
    }
}
