using System.Diagnostics;

namespace SampleDrawing.Renderers
{
    /// <summary>
    /// Abstract renderer
    /// </summary>
    public abstract class AbstractRenderer
    {
        private int _primitivesCount = 0;

        private readonly Stopwatch _watcher;

        public const string DefaultColorCode = "#000000";

        public int PrimitivesCount => _primitivesCount;

        public AbstractRenderer()
        {
            _watcher = new Stopwatch();
        }

        public void RenderRandomly(int count, string renderName = "")
        {
            _primitivesCount = count;

            if (string.IsNullOrEmpty(renderName))
                TestRender();
            else
                TestRender(renderName);
        }

        protected abstract void Render();

        public void TestRender()
        {
            _watcher.Reset();
            _watcher.Start();

            Render();

            _watcher.Stop();
        }

        public void TestRender(string testName)
        {
            TestRender();

            Debug.WriteLine($"Testing {testName}. Elapsed: {_watcher.ElapsedMilliseconds}ms");
        }
    }
}
