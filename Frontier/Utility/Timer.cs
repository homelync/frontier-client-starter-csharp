using System;
using System.Diagnostics;

namespace Frontier.Utility
{
    public class Timing : IDisposable
    {
        private readonly string metricName;
        private readonly IRichLogger logger;
        private readonly Stopwatch sw;

        public Timing(string metricName, IRichLogger logger)
        {
            this.metricName = metricName;
            this.logger = logger;
            sw = new Stopwatch();
            sw.Start();
        }
        public void Dispose()
        {
            sw.Stop();
            logger.LogDebug($"{metricName} took {sw.ElapsedMilliseconds}ms");
        }
    }
}