using System;
using Frontier.Utility;
using Xunit.Abstractions;

namespace Tests.Common.Logging
{
    public class TestLogger : IRichLogger
    {
        private readonly ITestOutputHelper testOutputHelper;
        public TestLogger(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }
        public void LogCritical(Exception exp, string message)
        {
            testOutputHelper.WriteLine(message + exp.Message);
        }

        public void LogCritical(string message)
        {
            testOutputHelper.WriteLine(message);
        }

        public void LogDebug(string message)
        {
            testOutputHelper.WriteLine(message);
        }

        public void LogError(Exception exp, string message)
        {
            testOutputHelper.WriteLine(message + exp.Message);
        }

        public void LogError(string message)
        {
            testOutputHelper.WriteLine(message);
        }

        public void LogInformation(string message)
        {
            testOutputHelper.WriteLine(message);
        }

        public void LogTrace(string message)
        {
            testOutputHelper.WriteLine(message);
        }

        public void LogWarning(string message)
        {
            testOutputHelper.WriteLine(message);
        }
    }
}