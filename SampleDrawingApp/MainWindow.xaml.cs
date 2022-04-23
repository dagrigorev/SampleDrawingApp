using SampleDrawing.IoC;
using SampleDrawing.Renderers;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;

namespace SampleDrawingApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IApplicationInitializer _entryPoint; 

        public MainWindow()
        {
            InitializeComponent();
            _entryPoint = DefaultRendererInitializer
                .CreateInitializer(GeneralCanvas)
                .Initialize();
        }

        /// <summary>
        /// Button clicked event handler.
        /// Starts drawing tests on <see cref="GeneralCanvas"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawX10_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {AsButton(sender).Content} primitives", 10);
        }

        /// <summary>
        /// Button clicked event handler.
        /// Starts drawing tests on <see cref="GeneralCanvas"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawX100_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {AsButton(sender).Content} primitives", 100);
        }

        /// <summary>
        /// Button clicked event handler.
        /// Starts drawing tests on <see cref="GeneralCanvas"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawX1k_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {AsButton(sender).Content} primitives", 1_000);
        }

        /// <summary>
        /// Button clicked event handler.
        /// Starts drawing tests on <see cref="GeneralCanvas"/>
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DrawX10k_Click(object sender, RoutedEventArgs e)
        {
            TestRender($"Drawing {AsButton(sender).Content} primitives", 10_000);
        }

        /// <summary>
        /// Runs and measures <see cref="Render(int)"/> method timings.
        /// </summary>
        /// <param name="testName">Test name</param>
        /// <param name="primitivesCount">Primitives count</param>
        private void TestRender(string testName, int primitivesCount)
        {
            Title = testName;

            _entryPoint
                .Container.GetService<AbstractRenderer>()
                .RenderRandomly(primitivesCount, testName);
        }


        static Button AsButton(object sender) => (Button)sender;
    }
}
