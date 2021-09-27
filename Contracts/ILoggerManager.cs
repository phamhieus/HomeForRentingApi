using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
  public interface ILoggerManager
  {
    void LogInfo(string mess);
    void LogWarn(string mess);
    void LogDebug(string mess);
    void LogError(string mess);
  }
}
