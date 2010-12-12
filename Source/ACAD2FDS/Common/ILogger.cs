namespace Common
{
    using System;

    public interface ILogger
    {
        void LogError(Exception ex);

        void LogInfo(string info);
    }
}