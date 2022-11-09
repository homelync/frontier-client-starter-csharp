using System;

namespace Frontier.Utility
{
    public interface IRichLogger
    {
        void LogDebug(string message);
        void LogInformation(string message);
        void LogWarning(string message);
        void LogTrace(string message);
        void LogCritical(Exception exp, string message);
        void LogCritical(string message);
        void LogError(Exception exp, string message);
        void LogError(string message);
    }
}