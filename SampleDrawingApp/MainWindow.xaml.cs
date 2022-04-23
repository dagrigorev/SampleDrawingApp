using SampleDrawing.IoC;
using SampleDrawing.Renderers;
using System;
using System.Diagnostics;
using System.Windows;
namespace SampleDrawingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IApplicationInitializer _applicationInitializer; 

        public MainWindow()
        {
            InitializeComponent();
            _applicationInitializer = DefaultRendererInitializer
                .CreateInitializer(GeneralCanvas)
                .Initialize();
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
        /// Runs and measures <see cref="Render(int)"/> method timings.
        /// </summary>
        /// <param name="testName">Test name</param>
        /// <param name="primitivesCount">Primitives count</param>
        private void TestRender(string testName, int primitivesCount)
        {
            Title = testName;

            _applicationInitializer
                .Container.GetService<AbstractRenderer>()
                .RenderRandomly(primitivesCount, testName);
        }
    }
}
