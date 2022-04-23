using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SampleDrawing.Renderers
{
    public class DirectCanvasRenderer : AbstractRenderer
    {
        private readonly Canvas _renderCanvas;
        private readonly Random _primitiveRandomizer;
        
        public Random PrimitiveRandomizer => _primitiveRandomizer;

        public DirectCanvasRenderer(Canvas canvas)
        {
            _renderCanvas = canvas;
            _primitiveRandomizer = new Random(0);
        }

        protected override void Render()
        {
            if (PrimitivesCount <= 0)
                throw new ArgumentException(nameof(PrimitivesCount));

            _renderCanvas.Children.Clear();
            _renderCanvas.Background = new SolidColorBrush(GetColorFromString(DefaultColorCode));

            for (var i = 0; i < PrimitivesCount; i++)
            {
                var randomStartPoint = GetRandomPoint(_renderCanvas.ActualWidth, _renderCanvas.ActualHeight);
                var randomEndPoint = GetRandomPoint(_renderCanvas.ActualWidth, _renderCanvas.ActualHeight);
                var randomColor = GetRandomColor();

                var randomPath = new Path
                {
                    Stroke = new SolidColorBrush(randomColor),
                    StrokeThickness = _primitiveRandomizer.Next(1, 20),
                    Data = new LineGeometry { StartPoint = randomStartPoint, EndPoint = randomEndPoint }
                };

                _renderCanvas.Children.Add(randomPath);
            }

            Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(() => { }));
        }

        /// <summary>
        /// Returns <see cref="Color"/> from color code string.
        /// </summary>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        public static Color GetColorFromString(string colorCode) => (Color)ColorConverter.ConvertFromString(colorCode);

        /// <summary>
        /// Returns random point
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        public Point GetRandomPoint(double maxWidth, double maxHeight) => new Point(_primitiveRandomizer.Next((int)maxWidth), _primitiveRandomizer.Next((int)maxHeight));

        /// <summary>
        /// Returns random color
        /// </summary>
        /// <returns></returns>
        public Color GetRandomColor() => Color.FromArgb(
                    (byte)((10 + _primitiveRandomizer.Next(255)) % 255),
                    (byte)_primitiveRandomizer.Next(255),
                    (byte)_primitiveRandomizer.Next(255),
                    (byte)_primitiveRandomizer.Next(255));
    }
}
