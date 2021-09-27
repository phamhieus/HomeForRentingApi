using Contracts;
using NLog;
using System;

namespace LoggerService
{
  public class LoggerManager : ILoggerManager
  {
    private static ILogger logger = LogManager.GetCurrentClassLogger();

    public void LogDebug(string mess)
    {
      logger.Debug(mess);
    }

    public void LogError(string mess)
    {
      logger.Error(mess);
    }

    public void LogInfo(string mess)
    {
      logger.Info(mess);
    }

    public void LogWarn(string mess)
    {
      logger.Warn(mess);
    }
  }
}
