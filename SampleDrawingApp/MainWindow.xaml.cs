using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace SampleDrawingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Random _primitiveRandomizer;
        private readonly Stopwatch _watcher;

        public int RandomPrimitivesCount => _primitiveRandomizer.Next() + 1;

        public MainWindow()
        {
            InitializeComponent();

            _primitiveRandomizer = new Random(0);
            _watcher = new Stopwatch();
        }

        private void DrawX10_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {DrawX10.Content} primitives", 10);
        }

        private void DrawX100_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {DrawX100.Content} primitives", 100);
        }

        private void DrawX1k_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {DrawX1k.Content} primitives", 1_000);
        }

        private void DrawX10k_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {DrawX10k.Content} primitives", 10_000);
        }

        /// <summary>
        /// Render primitives randomly on <see cref="GeneralCanvas"/>
        /// </summary>
        /// <param name="primitivesCount">Random primitives count</param>
        private void Render(int primitivesCount)
        {
            if (primitivesCount <= 0)
                throw new ArgumentException(nameof(primitivesCount));

            GeneralCanvas.Children.Clear();
            GeneralCanvas.Background = new SolidColorBrush(GetColorFromString("#000000"));

            for(var i = 0; i < primitivesCount; i++)
            {
                var randomStartPoint = GetRandomPoint(GeneralCanvas.ActualWidth, GeneralCanvas.ActualHeight);
                var randomEndPoint = GetRandomPoint(GeneralCanvas.ActualWidth, GeneralCanvas.ActualHeight);
                var randomColor = GetRandomColor();

                var randomPath = new Path
                {
                    Stroke = new SolidColorBrush(randomColor),
                    StrokeThickness = _primitiveRandomizer.Next(1, 20),
                    Data = new LineGeometry { StartPoint = randomStartPoint, EndPoint = randomEndPoint }
                };

                GeneralCanvas.Children.Add(randomPath);
            }

            Application.Current.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Render,
                new Action(() => { }));
        }

        /// <summary>
        /// Runs and measures <see cref="Render(int)"/> method timings.
        /// </summary>
        /// <param name="testName">Test name</param>
        /// <param name="primitivesCount">Primitives count</param>
        private void TestRender(string testName, int primitivesCount)
        {
            Title = testName;

            _watcher.Reset();
            _watcher.Start();

            Render(primitivesCount);

            _watcher.Stop();

            Debug.WriteLine($"Testing {Title}. Elapsed: {_watcher.ElapsedMilliseconds}ms");
        }

        /// <summary>
        /// Returns <see cref="Color"/> from color code string.
        /// </summary>
        /// <param name="colorCode"></param>
        /// <returns></returns>
        private static Color GetColorFromString(string colorCode) => (Color)ColorConverter.ConvertFromString(colorCode);

        /// <summary>
        /// Returns random point
        /// </summary>
        /// <param name="maxWidth"></param>
        /// <param name="maxHeight"></param>
        /// <returns></returns>
        private Point GetRandomPoint(double maxWidth, double maxHeight) => new Point(_primitiveRandomizer.Next((int)maxWidth), _primitiveRandomizer.Next((int)maxHeight));

        /// <summary>
        /// Returns random color
        /// </summary>
        /// <returns></returns>
        private Color GetRandomColor() => Color.FromArgb(
                    (byte)((10 + _primitiveRandomizer.Next(255)) % 255),
                    (byte)_primitiveRandomizer.Next(255),
                    (byte)_primitiveRandomizer.Next(255),
                    (byte)_primitiveRandomizer.Next(255));
    }
}
