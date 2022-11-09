using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Frontier.Utility
{
    public class RichLogger : IRichLogger
    {
        private readonly ILogger logger;
        private readonly IHostingEnvironment hostingEnvironment;

        public RichLogger(ILogger logger, IHostingEnvironment hostingEnvironment)
        {
            this.logger = logger;
            this.hostingEnvironment = hostingEnvironment;
        }

        public RichLogger(ILogger logger)
        {
            this.logger = logger;
        }

        public void LogDebug(string message)
        {
            logger.Log(LogLevel.Debug, new EventId(), message, null, (state, exception) => state.ToString());
        }

        public void LogInformation(string message)
        {
            logger.Log(LogLevel.Information, new EventId(100), message, null, (state, exception) => state.ToString());
        }

        public void LogWarning(string message)
        {
            logger.Log(LogLevel.Warning, new EventId(), message, null, (state, exception) => state.ToString());
        }

        public void LogTrace(string message)
        {
            logger.Log(LogLevel.Trace, new EventId(), message, null, (state, exception) => state.ToString());
        }

        public void LogCritical(Exception exp, string message)
        {
            logger.Log(LogLevel.Critical, new EventId(), message, exp, (state, exception) => state.ToString());
        }

        public void LogCritical(string message)
        {
            logger.Log(LogLevel.Critical, new EventId(), message, null, (state, exception) => state.ToString());
        }

        public void LogError(Exception exp, string message)
        {
            logger.Log(LogLevel.Error, new EventId(), message, exp, (state, exception) => state.ToString());
        }

        public void LogError(string message)
        {
            logger.Log(LogLevel.Error, new EventId(), message, null, (state, exception) => state.ToString());
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            logger.Log(logLevel, eventId, state, exception, formatter);
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logger.IsEnabled(logLevel);
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return logger.BeginScope(state);
        }
    }
}